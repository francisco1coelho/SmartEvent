using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEvent.Application.Interfaces;
using SmartEvent.Domain.Entities;

namespace SmartEvent.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ReservationsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var reservation = await _unitOfWork.Reservations.GetByIdAsync(id);

        if (reservation is null)
            return NotFound();

        return Ok(reservation);
    }

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