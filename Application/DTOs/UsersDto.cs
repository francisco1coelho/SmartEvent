using SmartEvent.Domain.Enums;
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

    public class  CreateUserDto
    {
        [Required]
        [StringLength(512)]
        public string Name { get; set; }
        [EmailAddress]
        [StringLength(512)]
        public string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(50)]
        public string Phone { get; set; }

        [Required]
        [StringLength(512)]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }

        [Required]
        public bool Locked { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}