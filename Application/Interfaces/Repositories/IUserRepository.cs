using SmartEvent.Domain.Entities;

namespace SmartEvent.Application.Interfaces.Repository;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<List<User>> GetAllAsync();
    void DeleteUser(User user);
    void CreateUser(User user);
}