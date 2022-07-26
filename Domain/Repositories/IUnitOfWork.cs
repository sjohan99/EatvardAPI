using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IRestaurantRepository Restaurants { get; }
    IPostRepository Posts { get; }
    IPostCommentRepository PostComments { get; }
    int Complete();
    Task<int> CompleteAsync();
}
