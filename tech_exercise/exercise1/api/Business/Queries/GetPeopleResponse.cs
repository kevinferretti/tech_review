using StargateAPI.Business.Dtos;

namespace StargateAPI.Business.Queries
{
    public class GetPeopleResponse : BaseResponse
    {
        public List<PersonAstronaut> People { get; set; } = new List<PersonAstronaut> { };

    }
}
