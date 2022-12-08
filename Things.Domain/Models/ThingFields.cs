namespace Things.Domain.Models;

/// <summary>
/// The non-ID fields of a thing.
/// </summary>
public class ThingFields
{
    /// <summary>
    /// The name of the thing.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// An optional description of the thing.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="ThingFields"/> type.
    /// </summary>
    /// <param name="name">The name of the thing.</param>
    /// <param name="description">An optional description of the thing.</param>
    public ThingFields(string name, string? description)
    {
        Name = name;
        Description = description;
    }
}
