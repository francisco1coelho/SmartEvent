// Application/Services/IReservationService.cs
using SmartEvent.Application.DTOs.ReservationsDto;
using SmartEvent.Domain.Entities;

namespace SmartEvent.Application.Services;

public interface IReservationService
{
    public Task<Reservation> CreateReservationAsync(CreateReservationDto reservation);
}