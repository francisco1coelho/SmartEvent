// Application/Services/IUserService.cs
using SmartEvent.Application.DTOs.Users;

namespace SmartEvent.Application.Services;

public interface IUserService
{
    Task<bool> UpdateProfileAsync(int userId, UpdateMeDto dto);
}