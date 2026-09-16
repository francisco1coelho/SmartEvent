using SmartEvent.Domain.Entities;

namespace SmartEvent.Application.Interfaces.Repository;

public interface IReservationRepository : IRepository<Reservation>
{
    Task<List<Reservation>> GetAllAsync();
    Task<Reservation?> GetReservationByIdAsync(int id);
    void CreateReservation(Reservation reservation);
    void UpdateReservation(Reservation reservation);
}