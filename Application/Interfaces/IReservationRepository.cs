using SmartEvent.Application.DTOs.ReservationsDto;
using SmartEvent.Domain.Entities;

namespace SmartEvent.Application.Interfaces;

public interface IReservationRepository : IRepository<Reservation>
{
    Task<List<Reservation>> GetAllAsync();
    Task<Reservation> AddReservationAsync(Reservation reservation);
    //Task<int> CountActiveByEventAsync(int eventId);
    //Task<List<Reservation>> GetByEventIdAsync(int eventId);
    //Task<List<Reservation>> GetByParticipantIdAsync(int participantId);
}