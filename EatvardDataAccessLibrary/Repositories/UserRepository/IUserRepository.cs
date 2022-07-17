using EatvardDataAccessLibrary.Repositories;
using EatvardDataAccessLibrary.Models;
using System.Linq.Expressions;

namespace EatvardDataAccessLibrary.Repositories.UserAccountRepository;

public interface IUserRepository : IGenericRepository<User>
{
    /*
    Task<IEnumerable<UserAccount>> GetUsers();
    Task<UserAccount> GetUser(int id);
    Task UpdateUser(UserAccount user);
    Task DeleteUser(int id);
    Task<UserAccount> CreateUser(UserAccount user);
    */
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> FindOneAsync(Expression<Func<User, bool>> expression);
}
