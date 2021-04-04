using Artium.Models.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artium.Models.Contexts
{
    public class CommentContext : DbContext
    {
        public DbSet<WallPost> WallPosts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Userpic> Userpics { get; set; }
        public CommentContext(DbContextOptions<CommentContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
