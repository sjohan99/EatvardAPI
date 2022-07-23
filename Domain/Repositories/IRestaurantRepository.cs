using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Repositories;
public interface IRestaurantRepository : IGenericRepository<Restaurant>
{
    Task<IEnumerable<Restaurant>> GetAllAsync();

    Task<Restaurant?> GetAsync(int id);

    Task<IEnumerable<Restaurant>> FindManyByName(string name);
}
