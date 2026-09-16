// Application/Services/IUserService.cs
using SmartEvent.Application.DTOs.Users;
using SmartEvent.Domain.Entities;

namespace SmartEvent.Application.Interfaces.Services;

public interface IUserService
{
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task<List<User>> GetAllAsync();
    Task<bool> UpdateProfileAsync(int userId, UpdateMeDto dto);
    Task<User> CreateUserAsync(CreateUserDto dto);
    Task DeleteAsync(int id);
}