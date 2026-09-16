// Application/Services/IEventService.cs
using SmartEvent.Application.DTOs.Events;
using SmartEvent.Domain.Entities;

namespace SmartEvent.Application.Interfaces.Services;

public interface IEventService
{
    Task<Event> CreateEventAsync(EventsRequestDto dto);
    Task<Event> UpdateEventAsync(int id, EventsRequestDto dto);
    Task<Event?> GetEventByIdAsync(int id);
    Task<List<Event>> GetAllEventsAsync();
    Task DeleteEventAsync(int id);
}