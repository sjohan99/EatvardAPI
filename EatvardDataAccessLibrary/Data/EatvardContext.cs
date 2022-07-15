using EatvardDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EatvardDataAccessLibrary.Data;

public class EatvardContext : DbContext
{
    string? ConnectionString;

    public EatvardContext() { }

    public EatvardContext(DbContextOptions<EatvardContext> options) : base(options) { }

    public EatvardContext(string connString)
    {
        ConnectionString = connString;
    }

    public DbSet<UserAccount> Users { get; set; } = null!;
    public DbSet<Restaurant> Restaurants { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (ConnectionString == null)
        {
            optionsBuilder.UseSqlServer();
        } 
        else
        {
            optionsBuilder.UseSqlServer(ConnectionString!);
        }
    }
}

