using Microsoft.EntityFrameworkCore;
using SmartEvent.Application.Interfaces;
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
}