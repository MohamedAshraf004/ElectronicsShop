using ElectronicsShop.IRepos;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace ElectronicsShop.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbContext Context;
    internal DbSet<T> dbSet;

    public Repository(DbContext context)
    {
        Context = context;
        this.dbSet = context.Set<T>();
    }

    public async Task<bool> AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        return await Context.SaveChangesAsync() > 0;
    }

    public async Task<T> GetAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Expression<Func<T, IQueryable<T>>> select = null,
        Expression<Func<T, T>> selector = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Expression<Func<T, bool>> includeFilter = null,
        string includeProperties = null)
    {
        IQueryable<T> query = dbSet.AsNoTracking();

        if (includeFilter is not null)
        {
            query = query.Include(includeFilter);
        }

        if (select != null)
        {
            query = (IQueryable<T>)query.Select(select);
        }
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                         StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }

        return await query.ToListAsync();
    }

    public async Task<bool> ExistAsync(int id)
    {
        var entity = await dbSet.FindAsync(id);
        return entity is not null;
    }

    public async Task<bool> ExistAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null)
    {
        IQueryable<T> query = dbSet.AsNoTracking();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                         StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        var result = await query.FirstOrDefaultAsync() is not null;
        return result;
    }

    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null,
        string includeProperties = null)
    {
        IQueryable<T> query = dbSet.AsNoTracking();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                         StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<bool> RemoveAsync(T entity)
    {
        try
        {
            dbSet.Remove(entity);
            return await Context.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> RemoveAsync(int id)
    {
        T removedEntity = await dbSet.FindAsync(id);
        return await RemoveAsync(removedEntity);

    }
}