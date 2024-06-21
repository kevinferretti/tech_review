using MediatR;

namespace StargateAPI.Business.Commands
{
    public class CreatePersonRequest : IRequest<CreatePersonResponse>
    {
        public required string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
    }
}
