using StargateAPI.Business.Dtos;

namespace StargateAPI.Business.Queries
{
    public class GetPersonByUsernameResponse : BaseResponse
    {
        public PersonAstronaut? Person { get; set; }
    }
}
