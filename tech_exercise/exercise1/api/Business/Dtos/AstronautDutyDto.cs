using StargateAPI.Business.Data;

namespace StargateAPI.Business.Dtos
{
    public class AstronautDutyDto
    {
        public AstronautDutyDto(AstronautDuty duty)
        {
            Rank = duty.Rank;
            Title = duty.Title;
            StartDate = duty.StartDate;
            EndDate = duty.EndDate;
        }

        public Rank Rank { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
