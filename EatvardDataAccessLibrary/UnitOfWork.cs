using EatvardDataAccessLibrary.Data;
using EatvardDataAccessLibrary.Repositories.UserAccountRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EatvardDataAccessLibrary;

public class UnitOfWork : IUnitOfWork
{
    private readonly EatvardContext _context;
    public IUserRepository Users { get; private set; }
    public UnitOfWork(EatvardContext context)
    {
        _context = context;
        Users = new UserRepository(context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
