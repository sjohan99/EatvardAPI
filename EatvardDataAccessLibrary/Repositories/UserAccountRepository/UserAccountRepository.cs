using EatvardDataAccessLibrary.Data;
using EatvardDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EatvardDataAccessLibrary.Repositories.UserAccountRepository;

public class UserAccountRepository : GenericRepository<UserAccount>, IUserAccountRepository
{
    public UserAccountRepository(EatvardContext context) : base(context)
    {
    }

    public async Task<UserAccount?> FindOneAsync(Expression<Func<UserAccount, bool>> expression)
    {
        return await Find(expression).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<UserAccount>> GetAllUsersAsync()
    {
        return await GetAll().ToListAsync();
    }

    public async Task<UserAccount?> GetUserByIdAsync(int id)
    {
        return await Find(user => user.Id == id).FirstOrDefaultAsync();
    }
}
