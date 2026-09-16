using Microsoft.EntityFrameworkCore;
using SmartEvent.Application.Interfaces.Repository;
using SmartEvent.Domain.Entities;
using SmartEvent.Infrastructure.Persistence;

namespace SmartEvent.Infrastructure.Repositories;

public class ReservationRepository : Repository<Reservation>, IReservationRepository
{
    public ReservationRepository(SmartEventDbContext context) : base(context)
    {
    }

    public async Task<List<Reservation>> GetAllAsync()
    {
        return await _context.Reservations.ToListAsync();
    }

    public async Task<Reservation?> GetReservationByIdAsync(int id)
    {
        return await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id);
    }

    public void CreateReservation(Reservation reservation)
    {
        _context.Reservations.Add(reservation);
    }

    public void UpdateReservation(Reservation reservation)
    {
        _context.Reservations.Update(reservation);
    }
}
