using Things.Domain.Models;
using Things.Domain.Services;

namespace Things.Database;

public class DataSource : IDataSource
{
    private readonly ThingsDbContext _dbContext;

    public DataSource(ThingsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IRepository<Guid, Thing> Things => new EntityRepository<Guid, Thing>(_dbContext);

    public Task SaveChangesAsync() => _dbContext.SaveChangesAsync();
}
