using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using Things.Domain.Services;

namespace Things.Database;

public class EntityRepository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : class
{
    private readonly ThingsDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public EntityRepository(ThingsDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetAsync(TKey id) => await _dbSet.FindAsync(id);

    public IAsyncEnumerable<TEntity> GetAsync(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int? skip = null,
        int? take = null)
    {
        var query = _dbSet.Where(filter);
        query = orderBy?.Invoke(query) ?? query;
        query = skip.HasValue ? query.Skip(skip.Value) : query;
        query = take.HasValue ? query.Take(take.Value) : query;
        return query.AsAsyncEnumerable();
    }

    public TEntity Add(TEntity entity) => _dbSet.Add(entity).Entity;

    public TEntity Update(TEntity entity)
    {
        var entry = _dbSet.Entry(entity);
        entry.State = EntityState.Modified;
        return entry.Entity;
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
}
