using MediatR;

namespace StargateAPI.Business.Queries
{
    public class GetPersonByUsernameRequest : IRequest<GetPersonByUsernameResponse>
    {
        public required string Username { get; set; } = string.Empty;
    }
}
