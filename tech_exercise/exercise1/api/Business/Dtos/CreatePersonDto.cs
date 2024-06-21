using System.ComponentModel.DataAnnotations;

namespace StargateAPI.Business.Dtos
{
    public class CreatePersonDto
    {
        [Required]
        [MaxLength(30)]
        [MinLength(4)]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username must only be alphanumeric")]
        public string Username { get; set; } = string.Empty;

        [MaxLength(100)]
        public string DisplayName { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }
    }
}
