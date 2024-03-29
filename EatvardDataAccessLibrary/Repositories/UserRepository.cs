﻿using Domain.Models;
using Domain.Repositories;
using EatvardDataAccessLibrary.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EatvardDataAccessLibrary.Repositories.UserRepository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(EatvardContext context) : base(context)
    {
    }

    public async Task<User?> FindOneAsync(Expression<Func<User, bool>> expression)
    {
        return await Find(expression).FirstOrDefaultAsync();
    }

    public async Task<User?> FindOneByEmail(string email)
    {
        return await Find(user => user.Email == email).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await GetAllAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await Find(user => user.Id == id).FirstOrDefaultAsync();
    }
}
