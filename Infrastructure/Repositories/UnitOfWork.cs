using SmartEvent.Application.Interfaces;
using SmartEvent.Infrastructure.Persistence;

namespace SmartEvent.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly SmartEventDbContext _context;

    public IUserRepository Users { get; }
    public ICategoryRepository Categories { get; }
    public IEventRepository Events { get; }
    public IReservationRepository Reservations { get; }

    public UnitOfWork(
        SmartEventDbContext context,
        IUserRepository users,
        ICategoryRepository categories,
        IEventRepository events,
        IReservationRepository reservations)
    {
        _context = context;
        Users = users;
        Categories = categories;
        Events = events;
        Reservations = reservations;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}