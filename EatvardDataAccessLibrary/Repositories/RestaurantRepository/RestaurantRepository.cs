using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EatvardDataAccessLibrary.Data;
using EatvardDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EatvardDataAccessLibrary.Repositories.RestaurantRepository;
public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
{
    public RestaurantRepository(EatvardContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Restaurant>> FindManyByName(string name)
    {
        name = name.ToLower().Trim();
        return await FindAsync(restaurant => restaurant.Name == name);
    }

    public async Task<Restaurant?> GetAsync(int id)
    {
        return await Find(restaurant => restaurant.Id == id).FirstOrDefaultAsync();
    }
}
