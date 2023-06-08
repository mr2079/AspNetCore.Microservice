using Order.Domain.Common;
using System.Linq.Expressions;

namespace Order.Appliction.Contracts.Persistence;

public interface IAsyncRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync();

    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate,
                                    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
                                    string? includeString,
                                    bool disableTracking = true);

    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate,
                                    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
                                    List<Expression<Func<T, object>>>? includes,
                                    bool disableTracking = true);

    Task<T> GetByIdAsync(int id);
    Task<T> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
