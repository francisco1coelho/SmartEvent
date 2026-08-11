using SmartEvent.Application.Interfaces;
using SmartEvent.Domain.Entities;
using SmartEvent.Infrastructure.Persistence;

namespace SmartEvent.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(SmartEventDbContext context) : base(context)
    {
    }
}

