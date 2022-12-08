using System.Linq.Expressions;

namespace Things.Domain.Services;

/// <summary>
/// A repository containing instances of <typeparamref name="TModel"/>.
/// </summary>
/// <typeparam name="TKey">
/// The type of the value or values used to identify instances of <typeparamref name="TModel"/>.
/// </typeparam>
public interface IRepository<TKey, TModel> where TModel : class
{
    /// <summary>
    /// Returns the <typeparamref name="TModel"/> identified by <paramref name="id"/> or
    /// <c>null</c> not found.
    /// </summary>
    Task<TModel?> GetAsync(TKey id);

    /// <summary>
    /// Returns the <typeparamref name="TModel"/> instances matching <paramref name="filter"/>.
    /// </summary>
    /// <param name="filter">
    /// A filter expression to select instances from the repository.
    /// </param>
    /// <param name="orderBy">
    /// An optional order for the instances to be returned in.
    /// </param>
    /// <param name="skip">
    /// An optional number of entities to skip.
    /// </param>
    /// <param name="take">
    /// An optional number of entities to take.
    /// </param>
    IAsyncEnumerable<TModel> GetAsync(
        Expression<Func<TModel, bool>> filter,
        Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null,
        int? skip = null,
        int? take = null);

    /// <summary>
    /// Adds <paramref name="entity"/> to the repository.
    /// </summary>
    TModel Add(TModel entity);

    /// <summary>
    /// Updates the <typeparamref name="TModel"/> whose ID matches <paramref name="entity"/> to
    /// match the given instance.
    /// </summary>
    TModel Update(TModel entity);

    /// <summary>
    /// Removes the <typeparamref name="TModel"/> whose ID matches <paramref name="entity"/>.
    /// </summary>
    void Delete(TModel entity);
}
