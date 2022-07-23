using Domain.Models;
using System.Linq.Expressions;

namespace Domain.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> FindOneAsync(Expression<Func<User, bool>> expression);
    Task<User?> FindOneByEmail(string email);
}
