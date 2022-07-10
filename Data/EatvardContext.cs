using EatvardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EatvardAPI.Data;

public class EatvardContext : DbContext
{
    string? connectionString;

    public EatvardContext() { }

    public EatvardContext(string connString)
    {
        connectionString = connString;
    }

    public DbSet<UserAccount> Users { get; set; } = null!;
    public DbSet<UserAuthentication> UserAuthentications { get; set; } = null!;
    public DbSet<Restaurant> Restaurants { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (connectionString == null)
        {
            throw new ArgumentNullException(nameof(connectionString));
        }
        else
        {
            optionsBuilder.UseSqlServer(connectionString!);
        }
    }
}


