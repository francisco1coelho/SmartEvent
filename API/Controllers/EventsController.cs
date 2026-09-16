using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEvent.Application.DTOs.Events;
using SmartEvent.Application.Interfaces.Services;

namespace SmartEvent.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    /// <summary>
    /// Retrieves an event by its ID.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var @event = await _eventService.GetEventByIdAsync(id);

        if (@event is null)
            return NotFound();

        return Ok(new EventsResponseDto
        {
            Id = @event.Id,
            Name = @event.Name,
            Description = @event.Description,
            StartDate = @event.StartDate,
            EndDate = @event.EndDate,
            MaxCapacity = @event.MaxCapacity,
            Location = @event.Location,
            CreatedAt = @event.CreatedAt,
            State = @event.State,
            CategoryId = @event.CategoryId,
            OrganizerId = @event.OrganizerId
        });
    }

    /// <summary>
    /// Retrieves all events.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var events = await _eventService.GetAllEventsAsync();
        return Ok(events.Select(@event => new EventsResponseDto
        {
            Id = @event.Id,
            Name = @event.Name,
            Description = @event.Description,
            StartDate = @event.StartDate,
            EndDate = @event.EndDate,
            MaxCapacity = @event.MaxCapacity,
            Location = @event.Location,
            CreatedAt = @event.CreatedAt,
            State = @event.State,
            CategoryId = @event.CategoryId,
            OrganizerId = @event.OrganizerId
        }));
    }

    /// <summary>
    /// Deletes an event by its ID. Only users with the "Admin" role are authorized to perform this action.
    /// </summary>
    //[Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }


    /// <summary>
    /// Update event information.
    /// 
    /// This endpoint allows updating the information of an event with the specified ID.
    /// Only users with the "Admin" or "Organizer" roles are authorized to perform this action.
    /// </summary>
    ///
    /// <param name="id">The ID of the event to update.</param>
    /// <param name="event">The updated event information in the request body.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
    //[Authorize(Roles = "Admin")]
    //[Authorize(Roles = "Organizer")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] EventsRequestDto @event)
    {
        try
        {
            var updatedEvent = await _eventService.UpdateEventAsync(id, @event);
            return Ok(new EventsResponseDto
            {
                Id = updatedEvent.Id,
                Name = updatedEvent.Name,
                Description = updatedEvent.Description,
                StartDate = updatedEvent.StartDate,
                EndDate = updatedEvent.EndDate,
                MaxCapacity = updatedEvent.MaxCapacity,
                Location = updatedEvent.Location,
                CreatedAt = updatedEvent.CreatedAt,
                State = updatedEvent.State,
                CategoryId = updatedEvent.CategoryId,
                OrganizerId = updatedEvent.OrganizerId
            });
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
}