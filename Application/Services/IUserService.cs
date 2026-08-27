// Application/Services/IUserService.cs
using SmartEvent.Application.DTOs.Users;
using SmartEvent.Domain.Entities;

namespace SmartEvent.Application.Services;

public interface IUserService
{
    Task<bool> UpdateProfileAsync(int userId, UpdateMeDto dto);

    Task<User> CreateUserAsync(CreateUserDto dto);
}