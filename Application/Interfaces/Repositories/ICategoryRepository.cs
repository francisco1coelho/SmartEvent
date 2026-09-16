using SmartEvent.Domain.Entities;

namespace SmartEvent.Application.Interfaces.Repository;

public interface ICategoryRepository : IRepository<Category>
{
    Task<List<Category>> GetAllAsync();
}