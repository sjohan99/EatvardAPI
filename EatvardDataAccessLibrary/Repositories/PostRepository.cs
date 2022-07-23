using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Repositories;
using EatvardDataAccessLibrary.Data;

namespace EatvardDataAccessLibrary.Repositories;
public class PostRepository : GenericRepository<Post>, IPostRepository
{
    public PostRepository(EatvardContext context) : base(context)
    {
    }
}
