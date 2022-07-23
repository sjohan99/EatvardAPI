using Domain.Models;
using EatvardDataAccessLibrary.Data.FluentExtensions;
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

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Restaurant> Restaurants { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    //public DbSet<Address> Addresses { get; set; } = null!;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.EnforceUniqueUserEmail();
    }

    
}

