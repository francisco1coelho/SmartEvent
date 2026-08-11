using SmartEvent.Domain.Entities;

namespace SmartEvent.Application.Interfaces;

public interface IReservationRepository : IRepository<Reservation>
{
    //Task<int> CountActiveByEventAsync(int eventId);
    //Task<List<Reservation>> GetByEventIdAsync(int eventId);
    //Task<List<Reservation>> GetByParticipantIdAsync(int participantId);
}