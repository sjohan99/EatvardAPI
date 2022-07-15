using EatvardDataAccessLibrary.Data;
using EatvardDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EatvardDataAccessLibrary.Repositories.UserAccountRepository;

public class UserAccountRepository : GenericRepository<UserAccount>, IUserAccountRepository
{
    public UserAccountRepository(EatvardContext context) : base(context)
    {
    }

    public async Task<IEnumerable<UserAccount>> GetAllUsersAsync()
    {
        return await GetAll().ToListAsync();
    }

    public async Task<UserAccount> GetUserByIdAsync(int id)
    {
        return await Find(user => user.Id == id).FirstOrDefaultAsync();
    }
}
