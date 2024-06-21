using Dapper;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;

namespace StargateAPI.Business.Commands
{
    public class CommandRequestPreProcessors : IRequestPreProcessor<CreateAstronautDutyRequest>,
                                               IRequestPreProcessor<CreatePersonRequest>
    {
        private readonly StargateDbContext _context;

        public CommandRequestPreProcessors(StargateDbContext context)
        {
            _context = context;
        }

        public async Task Process(CreateAstronautDutyRequest request, CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM [AstronautDuty] WHERE Title = @Title AND StartDate = @StartDate";
            var previousDuty = await _context.Connection.QueryFirstOrDefaultAsync<AstronautDuty>(query, new { request.Title, request.StartDate });

            if (previousDuty is not null) throw new InvalidOperationException("Duty already exists");
        }

        public async Task Process(CreatePersonRequest request, CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM [Person] WHERE Username = @Username";
            var person = await _context.Connection.QueryFirstOrDefaultAsync<Person>(query, new { request.Username });

            if (person is not null) throw new InvalidOperationException("Username already taken");
        }
    }
}
