using Things.Domain.Models;

namespace Things.Domain.Services;

public class ThingsService : IThingsService
{
    private readonly IDataSource _dataSource;

    public ThingsService(IDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public Task<Thing?> GetThingAsync(Guid id) => _dataSource.Things.GetAsync(id);

    public async Task<Thing> CreateThingAsync(ThingFields thing)
    {
        // The repository will set a real ID.
        var toAdd = new Thing(Guid.Empty, thing.Name, thing.Description);
        var added = _dataSource.Things.Add(toAdd);
        await _dataSource.SaveChangesAsync();
        return added;
    }

    public async Task<Thing?> UpdateThingAsync(Guid id, ThingFields thing)
    {
        var toUpdate = await GetThingAsync(id);
        if (toUpdate == null)
        {
            return null;
        }
        toUpdate.Name = thing.Name;
        toUpdate.Description = thing.Description;
        var updated = _dataSource.Things.Update(toUpdate);
        // Throws Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException if the thing doesn't
        // exist. We should return null but catching an EF feels like a layer violation.
        await _dataSource.SaveChangesAsync();
        return updated;
    }

    public async Task<bool> DeleteThingAsync(Guid id)
    {
        var toDelete = await GetThingAsync(id);
        if (toDelete == null)
        {
            return false;
        }
        // Throws Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException if the thing doesn't
        // exist. We should return null but catching an EF feels like a layer violation.
        _dataSource.Things.Delete(toDelete);
        await _dataSource.SaveChangesAsync();
        return true;
    }
}
