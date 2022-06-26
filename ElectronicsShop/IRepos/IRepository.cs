using System.Linq.Expressions;

namespace ElectronicsShop.IRepos;

public interface IRepository<T> where T : class
{
    Task<T> GetAsync(int id);

    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Expression<Func<T, IQueryable<T>>> select = null,
        Expression<Func<T, T>> selector = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Expression<Func<T, bool>> includeFilter = null,
        string includeProperties = null);


    Task<bool> ExistAsync(int id);
    Task<bool> ExistAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null);
    Task<T> GetFirstOrDefaultAsync(
        Expression<Func<T, bool>> filter = null,
        string includeProperties = null
    );
    Task<bool> AddAsync(T entity);
    Task<bool> RemoveAsync(T entity);
    Task<bool> RemoveAsync(int id);
}