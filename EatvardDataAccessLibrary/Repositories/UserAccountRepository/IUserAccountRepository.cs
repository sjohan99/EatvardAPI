using EatvardDataAccessLibrary.Repositories;
using EatvardDataAccessLibrary.Models;

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
    Task<IEnumerable<UserAccount>> GetByNameDescendingAsync();
}
