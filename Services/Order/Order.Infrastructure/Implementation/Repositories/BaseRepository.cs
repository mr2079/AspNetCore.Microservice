using Microsoft.EntityFrameworkCore;
using Order.Appliction.Contracts.Persistence;
using Order.Domain.Common;
using Order.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Order.Infrastructure.Implementation.Repositories;

public class BaseRepository<T> : IAsyncRepository<T> where T : BaseEntity
{
    private readonly OrderContext _context;
    private readonly DbSet<T> _dbSet;

    public BaseRepository(OrderContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    } 

    public async Task<IReadOnlyList<T>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        => await _dbSet.Where(predicate).ToListAsync();

    public async Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>>? predicate,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
        string? includeString,
        bool disableTracking = true)
    {
        var query = _dbSet.AsQueryable();

        if (disableTracking) query = query.AsNoTracking();

        if (!string.IsNullOrEmpty(includeString)) query = query.Include(includeString);

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null) return await orderBy(query).ToListAsync();

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>>? predicate,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
        List<Expression<Func<T, object>>>? includes,
        bool disableTracking = true)
    {
        var query = _dbSet.AsQueryable();

        if (disableTracking) query = query.AsNoTracking();

        if (includes != null) includes.Aggregate(query, (current, include) => current.Include(include));

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null) return await orderBy(query).ToListAsync();

        return await query.ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(int id)
        => await _dbSet.FindAsync(id);

    public async Task<T> CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
