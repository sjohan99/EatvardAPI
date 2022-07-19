using EatvardDataAccessLibrary.Data;
using EatvardDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EatvardDataAccessLibrary.Repositories.UserAccountRepository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(EatvardContext context) : base(context)
    {
    }

    public async Task<User?> FindOneAsync(Expression<Func<User, bool>> expression)
    {
        return await Find(expression).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await GetAllAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await Find(user => user.Id == id).FirstOrDefaultAsync();
    }
}
