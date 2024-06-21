using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;
using System.Net;

namespace StargateAPI.Business.Queries
{
    public class GetAstronautDutiesByUsernameResult : BaseResponse
    {
        public PersonAstronaut Person { get; set; }
        public List<AstronautDuty> AstronautDuties { get; set; } = new List<AstronautDuty>();
    }
}
