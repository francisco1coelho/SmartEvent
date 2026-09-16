using Microsoft.EntityFrameworkCore;
using SmartEvent.Application.Interfaces.Repository;
using SmartEvent.Domain.Entities;
using SmartEvent.Infrastructure.Persistence;

namespace SmartEvent.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(SmartEventDbContext context) : base(context)
    {
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }
}

