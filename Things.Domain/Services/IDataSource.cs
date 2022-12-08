using Things.Domain.Models;

namespace Things.Domain.Services;

/// <summary>
/// A data source containing things and things related to them.
/// </summary>
public interface IDataSource
{
    /// <summary>
    /// The repository of things.
    /// </summary>
    public IRepository<Guid, Thing> Things { get; }

    /// <summary>
    /// Saves all of the changes made to the data source.
    /// </summary>
    public Task SaveChangesAsync();
}
