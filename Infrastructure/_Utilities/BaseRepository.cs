using Domain.Common.Domain;
using Domain.Common.Domain.Repository;
using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure._Utilities;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity :BaseEntity
{
    protected readonly ShahriarBookStoreContext _context;

    public BaseRepository(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public async Task AddRange(ICollection<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
    }

    public bool Exists(Expression<Func<TEntity, bool>> expression)
    {
        return _context.Set<TEntity>().Any(expression);
    }

    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        return _context.Set<TEntity>().AnyAsync(expression);

    }
    //que to method
    public TEntity Get(long id)
    {
       return _context.Set<TEntity>().FirstOrDefault(x=>x.Id.Equals(id));
    }

    public async Task<TEntity> GetAsync(long id)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<TEntity> GetTracking(long id)
    {
        return await _context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<int> Save()
    {
        return await _context.SaveChangesAsync();

    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }
}
