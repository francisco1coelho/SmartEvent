using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEvent.Application.DTOs.Reservations;
using SmartEvent.Application.DTOs.ReservationsDto;
using SmartEvent.Application.Interfaces;
using SmartEvent.Application.Interfaces.Services;
using SmartEvent.Domain.Entities;

namespace SmartEvent.API.Controllers;

/// <summary>
/// Controller for managing reservations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    /// <summary>
    /// Gets all reservations.
    /// </summary>
    /// <returns> A list of all reservations. </returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var reservations = await _reservationService.GetAllAsync();
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
        var reservation = await _reservationService.GetByIdAsync(id);

        if (reservation is null)
            return NotFound();

        return Ok(reservation);
    }

    /// <summary>
    /// Creates a new reservation.
    /// </summary>
    /// <param name="reservation"></param>
    /// <returns>The created reservation.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDto reservation)
    {
        try
        {
            var createdReservation = await _reservationService.CreateReservationAsync(reservation);
            return CreatedAtAction(nameof(GetById), new { id = createdReservation.Id }, createdReservation);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Updates a reservation by its ID.
    /// 
    /// Only accessible to users with the "Admin" role.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="reservation"></param>
    /// <returns>The updated reservation.</returns>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateReservation(int id, [FromBody] UpdateReservationDto reservation)
    {
        try
        {
            var updatedReservation = await _reservationService.UpdateReservationAsync(id, reservation);
            return Ok(updatedReservation);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
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
        try
        {
            await _reservationService.DeleteReservationAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
}