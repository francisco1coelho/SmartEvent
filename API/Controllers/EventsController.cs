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
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var @event = await _eventService.GetEventByIdAsync(id);

        if (@event is null)
            return NotFound();

        return Ok(@event);
    }

    /// <summary>
    /// Retrieves all events.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var events = await _eventService.GetAllEventsAsync();
        return Ok(events);
    }

    /// <summary>
    /// Deletes an event by its ID. Only users with the "Admin" role are authorized to perform this action.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
            return Ok(updatedEvent);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

}