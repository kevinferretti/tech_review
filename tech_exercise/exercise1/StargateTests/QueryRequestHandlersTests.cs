using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;
using StargateAPI.Business.Queries;

namespace StargateTests.Queries
{
    public class QueryRequestHandlersTests : IDisposable
    {
        private const string TestUsername = "TesterSteve";
     
        private readonly QueryRequestHandlers _handler;
        private readonly StargateDbContext _context;
        private readonly SqliteConnection _connection;

        public QueryRequestHandlersTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<StargateDbContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new StargateDbContext(options);
            _context.Database.EnsureCreated();

            _handler = new QueryRequestHandlers(_context);
        }

        [Fact]
        public async Task Handle_GetPeopleRequest_ShouldGetPeople()
        {
            var person1 = new Person { Username = TestUsername };
            var person2 = new Person { Username = TestUsername + "2" };
            await _context.People.AddRangeAsync(person1, person2);
            await _context.SaveChangesAsync();
            var request = new GetPeopleRequest();

            var response = await _handler.Handle(request, CancellationToken.None);

            Assert.Equal(2, response.People.Count);
        }

        [Fact]
        public async Task Handle_GetPersonByUsernameRequest_ShouldGetPersonByUsername()
        {
            var person = new Person { Username = TestUsername };
            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();

            var astronautDetail = new AstronautDetail
            {
                PersonId = person.Id,
                Rank = Rank.ChiefMasterSergeant,
                Title = "Colonel",
                CareerStartDate = DateTime.Now.AddYears(-10),
                CareerEndDate = DateTime.Now.AddYears(-5)
            };
            await _context.AstronautDetails.AddAsync(astronautDetail);
            await _context.SaveChangesAsync();

            var request = new GetPersonByUsernameRequest { Username = TestUsername };

            var response = await _handler.Handle(request, CancellationToken.None);

            Assert.Equal(person.Id, response.Person.PersonId);
        }

        [Fact]
        public async Task Handle_GetAstronautDutiesByUsername_ShouldGetAstronautDutiesByUsername()
        {
            var person = new Person { Username = TestUsername };
            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();
            var astronautDetail = new AstronautDetail
            {
                PersonId = person.Id,
                Rank = Rank.ChiefMasterSergeant,
                Title = "Colonel",
                CareerStartDate = DateTime.Now.AddYears(-10),
            };
            await _context.AstronautDetails.AddAsync(astronautDetail);
            await _context.SaveChangesAsync();
            var astronautDuty1 = new AstronautDuty
            {
                PersonId = person.Id,
                Title = "Captain",
                Rank = Rank.MajorGeneral,
                StartDate = DateTime.Now.AddYears(-1)
            };
            var astronautDuty2 = new AstronautDuty
            {
                PersonId = person.Id,
                Title = "Colonel",
                Rank = Rank.ChiefMasterSergeant,
                StartDate = DateTime.Now
            };
            await _context.AstronautDuties.AddRangeAsync(astronautDuty1, astronautDuty2);
            await _context.SaveChangesAsync();
            var request = new GetAstronautDutiesByUsername { Username = TestUsername };

            var response = await _handler.Handle(request, CancellationToken.None);

            Assert.Equal(person.Id, response.Person.PersonId);
            Assert.Equal(2, response.AstronautDuties.Count);
            Assert.Equal(astronautDuty1.PersonId, response.AstronautDuties.First().PersonId);
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
