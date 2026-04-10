using System.Linq.Expressions;
using DataFlowHub.Domain.Entities.Base;
using DataFlowHub.Domain.Repositories.Interfaces;
using DataFlowHub.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DataFlowHub.Infrastructure.Repositories.Implementation;

public class GenericRepository<T>(DataFlowHubDbContext context) : IGenericRepository<T>
    where T : BaseEntity
{
    protected readonly DataFlowHubDbContext Context = context;
    protected readonly DbSet<T> DbSet = context.Set<T>();

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public virtual async Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<T> query = DbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
                     (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        var totalCount = await query.CountAsync();

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    public virtual async Task AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
    }

    public virtual void Update(T entity)
    {
        DbSet.Attach(entity);
        Context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Delete(T entity)
    {
        if (Context.Entry(entity).State == EntityState.Deleted)
        {
            DbSet.Attach(entity);
        }

        DbSet.Remove(entity);
    }

    public virtual async Task<bool> ExistsAsync(Guid id)
    {
        return await DbSet.AnyAsync(e => e.Id == id);
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        return await Context.SaveChangesAsync();
    }
}
