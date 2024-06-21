using Dapper;
using MediatR;
using StargateAPI.Business.Data;

namespace StargateAPI.Business.Commands
{
    public class CommandRequestHandlers : IRequestHandler<CreateAstronautDutyRequest, CreateAstronautDutyResponse>,
                                          IRequestHandler<CreatePersonRequest, CreatePersonResponse>
    {
        private readonly StargateDbContext _context;

        public CommandRequestHandlers(StargateDbContext context)
        {
            _context = context;
        }

        public async Task<CreateAstronautDutyResponse> Handle(CreateAstronautDutyRequest request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * FROM [Person] WHERE @Username = Username";

            var person = await _context.Connection.QueryFirstOrDefaultAsync<Person>(query, new { request.Username });
            if (person is null) throw new InvalidOperationException("Person does not exist");

            query = $"SELECT * FROM [AstronautDetail] WHERE @Id = PersonId";

            var astronautDetail = await _context.Connection.QueryFirstOrDefaultAsync<AstronautDetail>(query, new { person?.Id });

            if (astronautDetail is null)
            {
                astronautDetail = new AstronautDetail
                {
                    PersonId = person.Id,
                    Title = request.Title,
                    Rank = request.Rank,
                    CareerStartDate = request.StartDate.AddDays(-1).Date,
                    CareerEndDate = request.Title == "RETIRED" ? request.StartDate.AddDays(-1).Date : null
                };

                await _context.AstronautDetails.AddAsync(astronautDetail);
            }
            else
            {
                astronautDetail.Title = request.Title;
                astronautDetail.Rank = request.Rank;
                astronautDetail.CareerEndDate = request.Title == "RETIRED" ? request.StartDate.AddDays(-1).Date : null;

                _context.AstronautDetails.Update(astronautDetail);
            }

            query = $"SELECT * FROM [AstronautDuty] WHERE @Id = PersonId Order By StartDate Desc";

            var astronautDuty = await _context.Connection.QueryFirstOrDefaultAsync<AstronautDuty>(query, new { person?.Id });

            if (astronautDuty is not null)
            {
                if (astronautDuty.StartDate > request.StartDate) throw new InvalidOperationException("Duties must be added in order");

                astronautDuty.EndDate = request.StartDate.AddDays(-1).Date;
                _context.AstronautDuties.Update(astronautDuty);
            }

            var newAstronautDuty = new AstronautDuty()
            {
                PersonId = person.Id,
                Rank = request.Rank,
                Title = request.Title,
                StartDate = request.StartDate.Date,
                EndDate = null
            };

            await _context.AstronautDuties.AddAsync(newAstronautDuty);

            await _context.SaveChangesAsync();

            return new CreateAstronautDutyResponse()
            {
                Id = newAstronautDuty.Id
            };
        }

        public async Task<CreatePersonResponse> Handle(CreatePersonRequest request, CancellationToken cancellationToken)
        {
            var newPerson = new Person()
            {
                Username = request.Username,
                DisplayName = request.DisplayName,
                DateOfBirth = request.DateOfBirth
            };

            await _context.People.AddAsync(newPerson);

            await _context.SaveChangesAsync();

            return new CreatePersonResponse()
            {
                Id = newPerson.Id
            };
        }
    }
}
