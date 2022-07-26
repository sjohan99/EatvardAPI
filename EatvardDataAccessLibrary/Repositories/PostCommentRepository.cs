using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Repositories;
using EatvardDataAccessLibrary.Data;

namespace EatvardDataAccessLibrary.Repositories;
public class PostCommentRepository : GenericRepository<PostComment>, IPostCommentRepository
{
    public PostCommentRepository(EatvardContext context) : base(context)
    {
    }
}
