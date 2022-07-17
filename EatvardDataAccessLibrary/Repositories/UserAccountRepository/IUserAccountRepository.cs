using EatvardDataAccessLibrary.Repositories;
using EatvardDataAccessLibrary.Models;
using System.Linq.Expressions;

namespace EatvardDataAccessLibrary.Repositories.UserAccountRepository;

public interface IUserAccountRepository : IGenericRepository<UserAccount>
{
    /*
    Task<IEnumerable<UserAccount>> GetUsers();
    Task<UserAccount> GetUser(int id);
    Task UpdateUser(UserAccount user);
    Task DeleteUser(int id);
    Task<UserAccount> CreateUser(UserAccount user);
    */
    Task<IEnumerable<UserAccount>> GetAllUsersAsync();
    Task<UserAccount?> GetUserByIdAsync(int id);
    Task<UserAccount?> FindOneAsync(Expression<Func<UserAccount, bool>> expression);
}
