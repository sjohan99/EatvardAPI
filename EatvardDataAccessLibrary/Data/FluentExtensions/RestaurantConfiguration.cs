using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EatvardDataAccessLibrary.Data.FluentExtensions;
public static class RestaurantConfiguration
{
    public static void AddRestaurantNoActionDelete(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Restaurant>()
            .HasOne<Address>()
            .WithOne().OnDelete(DeleteBehavior.NoAction);
    }

    public static void AddRestaurantAddressRequired(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Restaurant>(builder =>
        {
            builder.HasKey(e => e.Id);

            builder.OwnsOne(r => r.Address);

            builder.Navigation(r => r.Address).IsRequired();
        });
    }
}
