// Application/Services/IReservationService.cs
using SmartEvent.Application.DTOs.ReservationsDto;
using SmartEvent.Domain.Entities;

namespace SmartEvent.Application.Interfaces.Services;

public interface IReservationService
{
    Task<Reservation> CreateReservationAsync(CreateReservationDto reservation);
    Task<Reservation> UpdateReservationAsync(int id, UpdateReservationDto reservation);
    Task<Reservation?> GetByIdAsync(int id);
    Task<List<Reservation>> GetAllAsync();
    Task DeleteReservationAsync(int id);
}