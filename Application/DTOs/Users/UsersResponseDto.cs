using SmartEvent.Domain.Enums;

namespace SmartEvent.Application.DTOs.Users;

public class UsersResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public Role Role { get; set; }

    public bool Locked { get; set; }

    public DateTime CreatedAt { get; set; }
}