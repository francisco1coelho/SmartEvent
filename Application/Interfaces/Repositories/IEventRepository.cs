using SmartEvent.Domain.Entities;

namespace SmartEvent.Application.Interfaces.Repository;

public interface IEventRepository : IRepository<Event>
{
    Task<List<Event>> GetAllAsync();
    Task<Event?> GetEventByIdAsync(int id);
    void CreateEvent(Event @event);
    void UpdateEvent(Event @event);
}