using EatvardDataAccessLibrary.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EatvardDataAccessLibrary.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly EatvardContext _context;
    public GenericRepository(EatvardContext context)
    {
        _context = context;
    }
    public void Create(T entity)
    {
        _context.Set<T>().Add(entity);
    }
    public void CreateMultiple(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
    }
    public IQueryable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }
    public IQueryable<T> GetAll()
    {
        return _context.Set<T>();
    }
    public T? GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }
    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
    public void DeleteMultiple(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await GetAll().ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
    {
        return await Find(expression).ToListAsync();
    }
}
