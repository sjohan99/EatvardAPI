using EatvardDataAccessLibrary.Data;
using EatvardDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EatvardDataAccessLibrary.Repositories.UserAccountRepository;

public class UserAccountRepository : GenericRepository<UserAccount>, IUserAccountRepository
{
    public UserAccountRepository(EatvardContext context) : base(context)
    {
    }

    public async Task<IEnumerable<UserAccount>> GetByNameDescendingAsync()
    {
        return await _context.Users.OrderByDescending(d => d.FirstName).ToListAsync();
    }

}
