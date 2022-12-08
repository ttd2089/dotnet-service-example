namespace Things.Domain.Models;

/// <summary>
/// A thing.
/// </summary>
public class Thing : ThingFields
{
    /// <summary>
    /// The maximum length for a thing's name.
    /// </summary>
    public const int MaxNameLength = 128;

    /// <summary>
    /// The maximum length for a thing's description.
    /// </summary>
    public const int MaxDescriptionLength = 4096;

    /// <summary>
    /// The Id of the thing.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="Thing"/> type.
    /// </summary>
    /// <param name="id">The Id of the thing.</param>
    /// <param name="name">The name of the thing.</param>
    /// <param name="description">An optional description of the thing.</param>
    public Thing(Guid id, string name, string? description)
        : base(name, description)
    {
        Id = id;
    }
}
