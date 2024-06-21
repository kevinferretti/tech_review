using MediatR;

namespace StargateAPI.Business.Queries
{
    public class GetAstronautDutiesByUsername : IRequest<GetAstronautDutiesByUsernameResult>
    {
        public string Username { get; set; } = string.Empty;
    }
}
