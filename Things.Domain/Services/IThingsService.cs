using Things.Domain.Models;

namespace Things.Domain.Services;

/// <summary>
/// A service defining the logical operations allowed to be performed on things.
/// </summary>
public interface IThingsService
{
    /// <summary>
    /// Returns the <see cref="Thing"/> whose ID is <paramref name="id"/> or <c>null</c> not found.
    /// </summary>
    Task<Thing?> GetThingAsync(Guid id);

    /// <summary>
    /// Creates a new thing with the given fields.
    /// </summary>
    Task<Thing> CreateThingAsync(ThingFields thing);

    /// <summary>
    /// Updates the <see cref="Thing"/> whose ID matches <paramref name="entity"/> to match
    /// <paramref name="thing"/> and returns the updated things, or <c>null</c> if not found.
    /// </summary>
    Task<Thing?> UpdateThingAsync(Guid id, ThingFields thing);

    /// <summary>
    /// Removes the <see cref="Thing"/> identified by <paramref name="id"/> if one
    /// exists and returns a bool indicating whether it was found.
    /// </summary>
    Task<bool> DeleteThingAsync(Guid id);
}