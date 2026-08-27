using SmartEvent.Domain.Entities;

namespace SmartEvent.Application.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<List<User?>> GetAllAsync();

    Task DeleteAsync(User user);
}