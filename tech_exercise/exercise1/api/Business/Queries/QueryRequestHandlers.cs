using Dapper;
using MediatR;
using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;

namespace StargateAPI.Business.Queries
{
    public class QueryRequestHandlers : IRequestHandler<GetAstronautDutiesByUsername, GetAstronautDutiesByUsernameResult>,
                                        IRequestHandler<GetPeopleRequest, GetPeopleResponse>,
                                        IRequestHandler<GetPersonByUsernameRequest, GetPersonByUsernameResponse>
    {
        private readonly StargateDbContext _context;

        public QueryRequestHandlers(StargateDbContext context)
        {
            _context = context;
        }

        public async Task<GetAstronautDutiesByUsernameResult> Handle(GetAstronautDutiesByUsername request, CancellationToken cancellationToken)
        {
            var result = new GetAstronautDutiesByUsernameResult();

            var query = $"SELECT a.Id as PersonId, a.Username, b.Rank, b.Title, b.CareerStartDate, b.CareerEndDate FROM [Person] a LEFT JOIN [AstronautDetail] b on b.PersonId = a.Id WHERE @Username = a.Username";

            var person = await _context.Connection.QueryFirstOrDefaultAsync<PersonAstronaut>(query, new { request.Username });

            result.Person = person;

            query = $"SELECT * FROM [AstronautDuty] WHERE @PersonId = PersonId Order By StartDate Desc";

            var duties = await _context.Connection.QueryAsync<AstronautDuty>(query, new { person?.PersonId });

            result.AstronautDuties = duties.ToList();

            return result;
        }

        public async Task<GetPeopleResponse> Handle(GetPeopleRequest request, CancellationToken cancellationToken)
        {
            var result = new GetPeopleResponse();

            var query = $"SELECT a.Id as PersonId, a.Username, a.DisplayName, a.DateOfBirth, b.Rank, b.Title, b.CareerStartDate, b.CareerEndDate FROM [Person] a LEFT JOIN [AstronautDetail] b on b.PersonId = a.Id";

            var people = await _context.Connection.QueryAsync<PersonAstronaut>(query);

            result.People = people.ToList();

            return result;
        }

        public async Task<GetPersonByUsernameResponse> Handle(GetPersonByUsernameRequest request, CancellationToken cancellationToken)
        {
            var result = new GetPersonByUsernameResponse();

            var query = $"SELECT a.Id as PersonId, a.Username, a.DisplayName, a.DateOfBirth, b.Rank, b.Title, b.CareerStartDate, b.CareerEndDate FROM [Person] a LEFT JOIN [AstronautDetail] b on b.PersonId = a.Id WHERE @Username = a.Username";

            var person = await _context.Connection.QueryAsync<PersonAstronaut>(query, new { request.Username });

            result.Person = person.FirstOrDefault();

            return result;
        }
    }
}
