using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EatvardDataAccessLibrary.Data.FluentExtensions;
public static class PostCommentConfiguration
{
    public static void AddPostCommentForeignKeys(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PostComment>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(pc => pc.AuthorId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<PostComment>()
            .HasOne<Post>()
            .WithMany()
            .HasForeignKey(pc => pc.PostId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
