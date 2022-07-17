using EatvardDataAccessLibrary.Repositories.UserAccountRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EatvardDataAccessLibrary;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    int Complete();
    Task<int> CompleteAsync();
}
