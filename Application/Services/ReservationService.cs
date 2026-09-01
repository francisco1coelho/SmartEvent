// Application/Services/ReservationService.cs
using SmartEvent.Application.DTOs.ReservationsDto;
using SmartEvent.Application.Interfaces;
using SmartEvent.Domain.Entities;

namespace SmartEvent.Application.Services;

public class ReservationService : IReservationService
{
    private readonly IUnitOfWork _unitOfWork;

    public ReservationService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Reservation> CreateReservationAsync(CreateReservationDto reservation)
    {
        var newReservation = new Reservation
        {
            CreatedAt = DateTime.Now,
            EventId = reservation.EventId,
            ParticipantId = reservation.ParticipantId,
            State = reservation.state
        };

        return await _unitOfWork.Reservations.AddReservationAsync(newReservation);
    }
}