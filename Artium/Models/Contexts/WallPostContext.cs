using Artium.Models.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artium.Models
{
    public class WallPostContext : DbContext
    {
        public DbSet<WallPost> WallPosts { get; set; }
        public DbSet<User> Users { get; set; }
        public WallPostContext(DbContextOptions<WallPostContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
