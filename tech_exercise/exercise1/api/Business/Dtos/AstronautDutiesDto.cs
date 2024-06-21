using NJsonSchema.Validation;
using StargateAPI.Business.Data;

namespace StargateAPI.Business.Dtos
{
    public class AstronautDutiesDto
    {
        public AstronautDutiesDto(PersonAstronaut person, IEnumerable<AstronautDuty> duties)
        {
            Person = person;
            Duties = duties.Select(x => new AstronautDutyDto(x));
        }

        public PersonAstronaut Person { get; set; }
        public IEnumerable<AstronautDutyDto>? Duties { get; set; }
    }
}
