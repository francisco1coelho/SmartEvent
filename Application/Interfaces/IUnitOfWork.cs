namespace SmartEvent.Application.Interfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    ICategoryRepository Categories { get; }
    IEventRepository Events { get; }
    IReservationRepository Reservations { get; }

    Task<int> SaveChangesAsync();
}