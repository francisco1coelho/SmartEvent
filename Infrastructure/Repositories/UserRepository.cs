using Microsoft.EntityFrameworkCore;
using SmartEvent.Application.Interfaces.Repository;
using SmartEvent.Domain.Entities;
using SmartEvent.Infrastructure.Persistence;

namespace SmartEvent.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(SmartEventDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public void DeleteUser(User user)
    {
        _context.Users.Remove(user);
    }

    public void CreateUser(User user)
    {
        _context.Users.Add(user);
    }
}
