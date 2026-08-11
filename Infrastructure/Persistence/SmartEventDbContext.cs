using Microsoft.EntityFrameworkCore;
using SmartEvent.Domain.Entities;

namespace SmartEvent.Infrastructure.Persistence
{

    public class SmartEventDbContext : DbContext //to communicate with DB
    {
        public SmartEventDbContext(DbContextOptions<SmartEventDbContext> options) //receives the configuration options for the DbContext (like: what database to use)
        : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Reservation> Reservations => Set<Reservation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) //used to configure the model (entities) and their relationships (only runs 1 time)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmartEventDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
