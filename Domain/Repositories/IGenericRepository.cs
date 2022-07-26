using System.Linq.Expressions;

namespace Domain.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    IQueryable<T> GetAll();
    IQueryable<T> Find(Expression<Func<T, bool>> expression);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
    void Create(T entity);
    void CreateMultiple(IEnumerable<T> entities);
    void Delete(T entity);
    void DeleteMultiple(IEnumerable<T> entities);
    void Update(T entity);
}
