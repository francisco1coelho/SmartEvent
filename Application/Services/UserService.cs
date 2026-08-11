// Application/Services/UserService.cs
using SmartEvent.Application.DTOs.Users;
using SmartEvent.Application.Interfaces;

namespace SmartEvent.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

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
}