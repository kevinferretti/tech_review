using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Data;

namespace StargateTests
{
    public class CommandRequestHandlersTests : IDisposable
    {
        private const string TestUsername = "TesterSteve";

        private readonly CommandRequestHandlers _handler;
        private readonly StargateDbContext _context;
        private readonly SqliteConnection _connection;

        public CommandRequestHandlersTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<StargateDbContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new StargateDbContext(options);
            _context.Database.EnsureCreated();

            _handler = new CommandRequestHandlers(_context);
        }

        [Fact]
        public async Task Handle_CreatePersonRequest_ShouldCreatePersonWithoutDob()
        {
            var request = new CreatePersonRequest
            {
                Username = TestUsername
            };

            var response = await _handler.Handle(request, CancellationToken.None);

            var person = await _context.People.FindAsync(response.Id);
            Assert.Equal(request.Username, person?.Username);
            Assert.Equal(request.DateOfBirth, person?.DateOfBirth);
        }

        [Fact]
        public async Task Handle_CreatePersonRequest_ShouldCreatePersonWithDob()
        {
            var request = new CreatePersonRequest
            {
                Username = TestUsername,
                DateOfBirth = new DateTime(DateTime.Now.Year - 30, 1, 1)
            };

            var response = await _handler.Handle(request, CancellationToken.None);

            var person = await _context.People.FindAsync(response.Id);
            Assert.Equal(request.Username, person?.Username);
            Assert.Equal(request.DateOfBirth, person?.DateOfBirth);
        }

        [Fact]
        public async Task Handle_CreateAstronautDutyRequest_ShouldAddDutyAndDetail()
        {
            var person = new Person { Username = TestUsername };
            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();
            var request = new CreateAstronautDutyRequest
            {
                Username = TestUsername,
                Title = "Captain",
                Rank = Rank.MajorGeneral,
                StartDate = DateTime.Now
            };

            var response = await _handler.Handle(request, CancellationToken.None);

            var astronautDuty = await _context.AstronautDuties
                .Include(x => x.Person)
                .ThenInclude(y => y.AstronautDetail)
                .FirstOrDefaultAsync(x => x.Id == response.Id);
            Assert.Equal(person.Id, astronautDuty.PersonId);
            Assert.Equal(request.Rank, astronautDuty.Rank);
            Assert.Equal(astronautDuty.Person.AstronautDetail.Title, astronautDuty.Title);
        }

        [Fact]
        public async Task Handle_CreateAstronautDutyRequest_ShouldAddMultipleDutiesAndUpdateExistingDetail()
        {
            var person = new Person { Username = TestUsername };
            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();
            var request = new CreateAstronautDutyRequest
            {
                Username = TestUsername,
                Title = "Captain",
                Rank = Rank.MajorGeneral,
                StartDate = DateTime.Now.AddYears(-1)
            };
            await _handler.Handle(request, CancellationToken.None);
            _context.Entry(person).State = EntityState.Detached;
            request = new CreateAstronautDutyRequest
            {
                Username = TestUsername,
                Title = "Colonel",
                Rank = Rank.ChiefMasterSergeant,
                StartDate = DateTime.Now
            };

            var response = await _handler.Handle(request, CancellationToken.None);

            var astronautDuty = await _context.AstronautDuties
                .Include(x => x.Person)
                .ThenInclude(y => y.AstronautDetail)
                .FirstOrDefaultAsync(x => x.Id == response.Id);
            Assert.Equal(person.Id, astronautDuty.PersonId);
            Assert.Equal(request.Rank, astronautDuty.Rank);
            Assert.Equal(request.Title, astronautDuty.Person.AstronautDetail.Title);
        }

        [Fact]
        public async Task Handle_CreateAstronautDutyRequest_ShouldRetireAstronautIfRetired()
        {
            var person = new Person { Username = TestUsername };
            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();
            var request = new CreateAstronautDutyRequest
            {
                Username = TestUsername,
                Title = "RETIRED",
                Rank = Rank.MajorGeneral,
                StartDate = DateTime.Now
            };

            var response = await _handler.Handle(request, CancellationToken.None);

            person = await _context.People
                .Include(x => x.AstronautDetail)
                .FirstOrDefaultAsync(x => x.Id == person.Id);
            var astronautDuty = await _context.AstronautDuties
                .Include(x => x.Person)
                .ThenInclude(y => y.AstronautDetail)
                .FirstOrDefaultAsync(x => x.Id == response.Id);
            Assert.Equal(person.Id, astronautDuty.PersonId);
            Assert.NotNull(person.AstronautDetail.CareerEndDate);
        }

        [Fact]
        public async Task Handle_CreateAstronautDutyRequest_ShouldNotRetireAstronautIfNotRetired()
        {
            var person = new Person { Username = TestUsername };
            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();
            var request = new CreateAstronautDutyRequest
            {
                Username = TestUsername,
                Title = "Captain",
                Rank = Rank.MajorGeneral,
                StartDate = DateTime.Now
            };

            var response = await _handler.Handle(request, CancellationToken.None);

            person = await _context.People
                .Include(x => x.AstronautDetail)
                .FirstOrDefaultAsync(x => x.Id == person.Id);
            var astronautDuty = await _context.AstronautDuties
                .Include(x => x.Person)
                .ThenInclude(y => y.AstronautDetail)
                .FirstOrDefaultAsync(x => x.Id == response.Id);
            Assert.Equal(person.Id, astronautDuty.PersonId);
            Assert.Null(person.AstronautDetail.CareerEndDate);
        }

        [Fact]
        public async Task Handle_CreateAstronautDutyRequest_ShouldAdjustPreviousDutyEndDate()
        {
            var person = new Person { Username = TestUsername };
            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();
            var request1 = new CreateAstronautDutyRequest
            {
                Username = TestUsername,
                Title = "Captain",
                Rank = Rank.MajorGeneral,
                StartDate = DateTime.Now
            };
            var response = await _handler.Handle(request1, CancellationToken.None);
            _context.Entry(person).State = EntityState.Detached;
            var request2 = new CreateAstronautDutyRequest
            {
                Username = TestUsername,
                Title = "Colonel",
                Rank = Rank.ChiefMasterSergeant,
                StartDate = DateTime.Now
            };

            await _handler.Handle(request2, CancellationToken.None);

            var astronautDuty = await _context.AstronautDuties
                .Include(x => x.Person)
                .ThenInclude(y => y.AstronautDetail)
                .FirstOrDefaultAsync(x => x.Id == response.Id);
            Assert.Equal(person.Id, astronautDuty.PersonId);
            Assert.Equal(request1.Title, astronautDuty.Title);
            Assert.NotNull(astronautDuty.EndDate);
        }

        [Fact]
        public async Task Handle_CreateAstronautDutyRequest_ShouldNotAdjustNewestDutyEndDate()
        {
            var person = new Person { Username = TestUsername };
            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();
            var request1 = new CreateAstronautDutyRequest
            {
                Username = TestUsername,
                Title = "Captain",
                Rank = Rank.MajorGeneral,
                StartDate = DateTime.Now.AddYears(-1)
            };
            var response = await _handler.Handle(request1, CancellationToken.None);
            _context.Entry(person).State = EntityState.Detached;
            var request2 = new CreateAstronautDutyRequest
            {
                Username = TestUsername,
                Title = "Colonel",
                Rank = Rank.ChiefMasterSergeant,
                StartDate = DateTime.Now
            };

            response = await _handler.Handle(request2, CancellationToken.None);

            var astronautDuty = await _context.AstronautDuties
                .Include(x => x.Person)
                .ThenInclude(y => y.AstronautDetail)
                .FirstOrDefaultAsync(x => x.Id == response.Id);
            Assert.Equal(person.Id, astronautDuty.PersonId);
            Assert.Equal(request2.Title, astronautDuty.Title);
            Assert.Null(astronautDuty.EndDate);
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}