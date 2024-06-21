using MediatR;
using StargateAPI.Business.Data;

namespace StargateAPI.Business.Commands
{
    public class CreateAstronautDutyRequest : IRequest<CreateAstronautDutyResponse>
    {
        public required string Username { get; set; } = string.Empty;

        public required Rank Rank { get; set; }

        public required string Title { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
    }
}
