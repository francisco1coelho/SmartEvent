using SmartEvent.Domain.Entities;

namespace SmartEvent.Application.Interfaces;

public interface IEventRepository : IRepository<Event>
{
    //Task<List<Event>> GetActiveEventsAsync();
    //Task<List<Event>> SearchAsync(string? name, int? categoryId, DateTime? date);
}