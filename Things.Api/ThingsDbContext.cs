using Microsoft.EntityFrameworkCore;

namespace Things.Data;

public class ThingsDbContext : DbContext
{
    public DbSet<Thing> Things { get; set; }

    public ThingsDbContext(DbContextOptions<ThingsDbContext> options) : base(options)
    {
    }
}
