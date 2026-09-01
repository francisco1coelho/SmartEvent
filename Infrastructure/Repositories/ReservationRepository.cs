using Microsoft.EntityFrameworkCore;
using SmartEvent.Application.Interfaces;
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

    public async Task<Reservation> AddReservationAsync(Reservation reservation)
    {
        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();
        return reservation;
    }
}
