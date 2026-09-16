using Microsoft.EntityFrameworkCore;
using SmartEvent.Application.Interfaces.Repository;
using SmartEvent.Domain.Entities;
using SmartEvent.Infrastructure.Persistence;

namespace SmartEvent.Infrastructure.Repositories;

public class EventRepository : Repository<Event>, IEventRepository
{
    public EventRepository(SmartEventDbContext context) : base(context)
    {
    }

    public async Task<List<Event>> GetAllAsync()
    {
        return await _context.Events.ToListAsync();
    }

    public async Task<Event?> GetEventByIdAsync(int id)
    {
        return await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
    }

    public void CreateEvent(Event @event)
    {
        _context.Events.Add(@event);
    }

    public void UpdateEvent(Event @event)
    {
        _context.Events.Update(@event);
    }
}

