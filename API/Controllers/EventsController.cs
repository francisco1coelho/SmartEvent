using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEvent.Application.Interfaces;
using SmartEvent.Domain.Entities;

namespace SmartEvent.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public EventsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Retrieves an event by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var @event = await _unitOfWork.Events.GetByIdAsync(id);

        if (@event is null)
            return NotFound();

        return Ok(@event);
    }

    /// <summary>
    /// Deletes an event by its ID. Only users with the "Admin" role are authorized to perform this action.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var @event = await _unitOfWork.Events.GetByIdAsync(id);

        if (@event is null)
            return NotFound();

        _unitOfWork.Events.Remove(@event);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
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
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Organizer")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Event @event)
    {
        var existingEvent = await _unitOfWork.Events.GetByIdAsync(id);

        if (existingEvent is null)
            return NotFound();

        existingEvent.Name = @event.Name;
        existingEvent.Description = @event.Description;
        existingEvent.StartDate = @event.StartDate;
        existingEvent.EndDate = @event.EndDate;
        existingEvent.MaxCapacity = @event.MaxCapacity;
        existingEvent.Location = @event.Location;
        existingEvent.CategoryId = @event.CategoryId;
        existingEvent.OrganizerId = @event.OrganizerId;
        existingEvent.State = @event.State;

        _unitOfWork.Events.Update(existingEvent);
        await _unitOfWork.SaveChangesAsync();

        return Ok(existingEvent);
    }

}