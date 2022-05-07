using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Persistent.Models.Abstract;
using PriceObserver.Data.Persistent.Repositories.Abstract;

namespace PriceObserver.Data.Persistent.Repositories.Concrete;

public abstract class RepositoryBase<T> : IRepository<T> where T : class, IAggregateRoot
{
    protected readonly ApplicationDbContext Context;

    protected RepositoryBase(ApplicationDbContext context)
    {
        Context = context;
    }

    public virtual async Task<T> GetById(int id)
    {
        var entity = await Context.Set<T>().FindAsync(id);
        
        Context.DetachEntity(entity);

        return entity;
    }

    public virtual async Task<IList<T>> GetAll()
    {
        return await Context
            .Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }

    public virtual async Task Add(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();
        
        Context.DetachEntity(entity);
    }

    public virtual async Task Update(T entity)
    {
        Context.Set<T>().Update(entity);
        await Context.SaveChangesAsync();
        
        Context.DetachEntity(entity);
    }

    public virtual async Task Delete(T entity)
    {
        Context.Set<T>().Remove(entity);
        await Context.SaveChangesAsync();
    }
}