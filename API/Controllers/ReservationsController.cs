using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEvent.Application.Interfaces;
using SmartEvent.Domain.Entities;

namespace SmartEvent.API.Controllers;

/// <summary>
/// Controller for managing reservations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ReservationsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Gets a reservation by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var reservation = await _unitOfWork.Reservations.GetByIdAsync(id);

        if (reservation is null)
            return NotFound();

        return Ok(reservation);
    }

    /// <summary>
    /// Deletes a reservation by its ID. Only accessible to users with the "Admin" role.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var reservation = await _unitOfWork.Reservations.GetByIdAsync(id);

        if (reservation is null)
            return NotFound();

        _unitOfWork.Reservations.Remove(reservation);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}