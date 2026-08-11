using SmartEvent.Application.Interfaces;
using SmartEvent.Domain.Entities;
using SmartEvent.Infrastructure.Persistence;

namespace SmartEvent.Infrastructure.Repositories;
public class ReservationRepository : Repository<Reservation>, IReservationRepository
{
    public ReservationRepository(SmartEventDbContext context) : base(context)
    {
    }
}
