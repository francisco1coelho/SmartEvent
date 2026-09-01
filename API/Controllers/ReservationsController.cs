using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEvent.Application.DTOs.ReservationsDto;
using SmartEvent.Application.Interfaces;
using SmartEvent.Application.Services;
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
    private readonly IReservationService _reservationService;

    public ReservationsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _reservationService = new ReservationService(unitOfWork);
    }

    /// <summary>
    /// Gets all reservations.
    /// </summary>
    /// <returns> A list of all reservations. </returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var reservations = await _unitOfWork.Reservations.GetAllAsync();
        return Ok(reservations);
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

    [HttpPost]
    public async Task<Reservation> CreateReservation([FromBody] CreateReservationDto reservation)
    {
        var createdReservation = await _reservationService.CreateReservationAsync(reservation);
        return createdReservation;
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