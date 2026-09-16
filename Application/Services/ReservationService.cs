// Application/Services/ReservationService.cs
using SmartEvent.Application.DTOs.ReservationsDto;
using SmartEvent.Application.Interfaces;
using SmartEvent.Application.Interfaces.Services;
using SmartEvent.Domain.Entities;

namespace SmartEvent.Application.Services;

public class ReservationService : IReservationService
{
    private readonly IUnitOfWork _unitOfWork;

    public ReservationService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Reservation> CreateReservationAsync(CreateReservationDto reservation)
    {
        // Validate that event exists
        var eventExists = await _unitOfWork.Events.GetByIdAsync(reservation.EventId);
        if (eventExists == null)
        {
            throw new ArgumentException($"Event with ID {reservation.EventId} does not exist.");
        }

        // Validate that participant exists
        var participantExists = await _unitOfWork.Users.GetByIdAsync(reservation.ParticipantId);
        if (participantExists == null)
        {
            throw new ArgumentException($"User with ID {reservation.ParticipantId} does not exist.");
        }

        var newReservation = new Reservation
        {
            CreatedAt = DateTime.UtcNow,
            EventId = reservation.EventId,
            ParticipantId = reservation.ParticipantId,
            State = reservation.state
        };

        _unitOfWork.Reservations.CreateReservation(newReservation);
        await _unitOfWork.SaveChangesAsync();
        return newReservation;
    }

    public async Task<Reservation> UpdateReservationAsync(int id, UpdateReservationDto reservation)
    {
        var existingReservation = await _unitOfWork.Reservations.GetReservationByIdAsync(id);
        if (existingReservation == null)
        {
            throw new ArgumentException($"Reservation with ID {id} not found.");
        }

        // Validate that event exists if EventId is being changed
        if (existingReservation.EventId != reservation.EventId)
        {
            var eventExists = await _unitOfWork.Events.GetByIdAsync(reservation.EventId);
            if (eventExists == null)
            {
                throw new ArgumentException($"Event with ID {reservation.EventId} does not exist.");
            }
        }

        existingReservation.EventId = reservation.EventId;
        existingReservation.ParticipantId = reservation.ParticipantId;
        existingReservation.State = reservation.state;

        _unitOfWork.Reservations.UpdateReservation(existingReservation);
        await _unitOfWork.SaveChangesAsync();
        return existingReservation;
    }

    public async Task<Reservation?> GetByIdAsync(int id)
    {
        return await _unitOfWork.Reservations.GetReservationByIdAsync(id);
    }

    public async Task<List<Reservation>> GetAllAsync()
    {
        return await _unitOfWork.Reservations.GetAllAsync();
    }

    public async Task DeleteReservationAsync(int id)
    {
        var reservation = await _unitOfWork.Reservations.GetReservationByIdAsync(id);
        if (reservation == null)
        {
            throw new ArgumentException($"Reservation with ID {id} not found.");
        }

        _unitOfWork.Reservations.Remove(reservation);
        await _unitOfWork.SaveChangesAsync();
    }
}