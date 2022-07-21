﻿using EatvardDataAccessLibrary.Data;
using EatvardDataAccessLibrary.Repositories.RestaurantRepository;
using EatvardDataAccessLibrary.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;
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
    public IRestaurantRepository Restaurants { get; private set; }
    public UnitOfWork(EatvardContext context,
                      IUserRepository userRepository,
                      IRestaurantRepository restaurantRepository)
    {
        _context = context;
        Users = userRepository;
        Restaurants = restaurantRepository;
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
