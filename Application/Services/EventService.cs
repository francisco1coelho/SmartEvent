// Application/Services/EventService.cs
using SmartEvent.Application.DTOs.Events;
using SmartEvent.Application.Interfaces;
using SmartEvent.Application.Interfaces.Services;
using SmartEvent.Domain.Entities;

namespace SmartEvent.Application.Services;

public class EventService : IEventService
{
    private readonly IUnitOfWork _unitOfWork;

    public EventService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Event> CreateEventAsync(EventsRequestDto dto)
    {
        // Validate that organizer/user exists
        var organizer = await _unitOfWork.Users.GetByIdAsync(dto.OrganizerId);
        if (organizer == null)
        {
            throw new ArgumentException($"User with ID {dto.OrganizerId} does not exist.");
        }

        // Validate that category exists
        var category = await _unitOfWork.Categories.GetByIdAsync(dto.CategoryId);
        if (category == null)
        {
            throw new ArgumentException($"Category with ID {dto.CategoryId} does not exist.");
        }

        // Validate dates
        if (dto.StartDate >= dto.EndDate)
        {
            throw new ArgumentException("Start date must be before end date.");
        }

        var newEvent = new Event
        {
            Name = dto.Name,
            Description = dto.Description,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            MaxCapacity = dto.MaxCapacity,
            Location = dto.Location,
            CreatedAt = DateTime.UtcNow,
            State = dto.State,
            CategoryId = dto.CategoryId,
            OrganizerId = dto.OrganizerId
        };

        _unitOfWork.Events.CreateEvent(newEvent);
        await _unitOfWork.SaveChangesAsync();
        return newEvent;
    }

    public async Task<Event> UpdateEventAsync(int id, EventsRequestDto dto)
    {
        var existingEvent = await _unitOfWork.Events.GetEventByIdAsync(id);
        if (existingEvent == null)
        {
            throw new ArgumentException($"Event with ID {id} not found.");
        }

        // Validate dates
        if (dto.StartDate >= dto.EndDate)
        {
            throw new ArgumentException("Start date must be before end date.");
        }

        // Validate that category still exists if changed
        if (existingEvent.CategoryId != dto.CategoryId)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(dto.CategoryId);
            if (category == null)
            {
                throw new ArgumentException($"Category with ID {dto.CategoryId} does not exist.");
            }
        }

        existingEvent.Name = dto.Name;
        existingEvent.Description = dto.Description;
        existingEvent.StartDate = dto.StartDate;
        existingEvent.EndDate = dto.EndDate;
        existingEvent.MaxCapacity = dto.MaxCapacity;
        existingEvent.Location = dto.Location;
        existingEvent.State = dto.State;
        existingEvent.CategoryId = dto.CategoryId;

        _unitOfWork.Events.UpdateEvent(existingEvent);
        await _unitOfWork.SaveChangesAsync();
        return existingEvent;
    }

    public async Task<Event?> GetEventByIdAsync(int id)
    {
        return await _unitOfWork.Events.GetEventByIdAsync(id);
    }

    public async Task<List<Event>> GetAllEventsAsync()
    {
        return await _unitOfWork.Events.GetAllAsync();
    }

    public async Task DeleteEventAsync(int id)
    {
        var @event = await _unitOfWork.Events.GetEventByIdAsync(id);
        if (@event == null)
        {
            throw new ArgumentException($"Event with ID {id} not found.");
        }

        _unitOfWork.Events.Remove(@event);
        await _unitOfWork.SaveChangesAsync();
    }
}