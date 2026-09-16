// Application/Services/UserService.cs
using SmartEvent.Application.DTOs.Users;
using SmartEvent.Application.Interfaces;
using SmartEvent.Application.Interfaces.Services;
using SmartEvent.Domain.Entities;

namespace SmartEvent.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _unitOfWork.Users.GetByIdAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _unitOfWork.Users.GetByEmailAsync(email);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _unitOfWork.Users.GetAllAsync();
    }

    public async Task<bool> UpdateProfileAsync(int userId, UpdateMeDto dto)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user is null) return false;

        user.Name = dto.Name;
        user.Email = dto.Email;
        user.Phone = dto.Phone;

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<User> CreateUserAsync(CreateUserDto dto)
    {
        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            PasswordHash = dto.Password,
            Role = dto.Role,
            Locked = false,
            CreatedAt = DateTime.UtcNow
        };

        _unitOfWork.Users.CreateUser(user);
        await _unitOfWork.SaveChangesAsync();
        return user;
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user is null)
        {
            throw new ArgumentException($"User with ID {id} not found.");
        }

        _unitOfWork.Users.DeleteUser(user);
        await _unitOfWork.SaveChangesAsync();
    }
}