using System.ComponentModel.DataAnnotations;

namespace SmartEvent.Application.DTOs.Users
{
    public class UpdateMeDto
    {
        [Required]
        [StringLength(512)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(512)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(50)]
        public string Phone { get; set; } = string.Empty;
    }
}