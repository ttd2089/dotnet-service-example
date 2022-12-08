using Microsoft.EntityFrameworkCore;
using Things.Domain.Models;

namespace Things.Database;

public class ThingsDbContext : DbContext
{
    public DbSet<Thing> Things { get; set; }

    public ThingsDbContext(DbContextOptions<ThingsDbContext> options) : base(options)
    {
    }
}
