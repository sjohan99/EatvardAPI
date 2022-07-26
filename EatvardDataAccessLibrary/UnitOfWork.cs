using Domain.Repositories;
using EatvardDataAccessLibrary.Data;
using Microsoft.EntityFrameworkCore;

namespace EatvardDataAccessLibrary;

public class UnitOfWork : IUnitOfWork
{
    private readonly EatvardContext _context;
    public IUserRepository Users { get; private set; }
    public IRestaurantRepository Restaurants { get; private set; }
    public IPostRepository Posts { get; private set; }
    public IPostCommentRepository PostComments { get; private set; }

    public UnitOfWork(EatvardContext context,
                      IUserRepository userRepository,
                      IRestaurantRepository restaurantRepository,
                      IPostRepository postRepository,
                      IPostCommentRepository postCommentRepository)
    {
        _context = context;
        Users = userRepository;
        Restaurants = restaurantRepository;
        Posts = postRepository;
        PostComments = postCommentRepository;
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
        try
        {
            return await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return 0;
        }
    }
}
