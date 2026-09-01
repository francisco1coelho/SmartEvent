using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SmartEvent.Infrastructure.Persistence;

namespace SmartEvent.Infrastructure;

public class SmartEventDbContextFactory : IDesignTimeDbContextFactory<SmartEventDbContext>
{
    public SmartEventDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SmartEventDbContext>();

        var connectionString = "Host=localhost;Port=5432;Database=smartevent;Username=smartevent;Password=smartevent123";

        optionsBuilder.UseNpgsql(connectionString, b =>
            b.MigrationsAssembly(typeof(SmartEventDbContext).Assembly.FullName));

        return new SmartEventDbContext(optionsBuilder.Options);
    }
}