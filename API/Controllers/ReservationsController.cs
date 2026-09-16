using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEvent.Application.DTOs.Reservations;
using SmartEvent.Application.Interfaces.Services;

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
        return Ok(reservations.Select(reservation => new ReservationsResponseDto
        {
            Id = reservation.Id,
            CreatedAt = reservation.CreatedAt,
            CancelledAt = reservation.CancelledAt,
            State = reservation.State,
            EventId = reservation.EventId,
            ParticipantId = reservation.ParticipantId
        }));
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

        return Ok(new ReservationsResponseDto
        {
            Id = reservation.Id,
            CreatedAt = reservation.CreatedAt,
            CancelledAt = reservation.CancelledAt,
            State = reservation.State,
            EventId = reservation.EventId,
            ParticipantId = reservation.ParticipantId
        });
    }

    /// <summary>
    /// Creates a new reservation.
    /// </summary>
    /// <param name="reservation"></param>
    /// <returns>The created reservation.</returns>
    [HttpPost]
    public async Task<ActionResult<ReservationsResponseDto>> CreateReservation([FromBody] CreateReservationDto reservation)
    {
        try
        {
            var createdReservation = await _reservationService.CreateReservationAsync(reservation);
            var response = new ReservationsResponseDto
            {
                Id = createdReservation.Id,
                CreatedAt = createdReservation.CreatedAt,
                CancelledAt = createdReservation.CancelledAt,
                State = createdReservation.State,
                EventId = createdReservation.EventId,
                ParticipantId = createdReservation.ParticipantId
            };

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
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
            return Ok(new ReservationsResponseDto
            {
                Id = updatedReservation.Id,
                CreatedAt = updatedReservation.CreatedAt,
                CancelledAt = updatedReservation.CancelledAt,
                State = updatedReservation.State,
                EventId = updatedReservation.EventId,
                ParticipantId = updatedReservation.ParticipantId
            });
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