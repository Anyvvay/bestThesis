using Artium.Models.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artium.Models.Contexts
{
    public class UserFollowerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<UserFollower> Followers { get; set; }
        public DbSet<Userpic> Userpics { get; set; }
        public UserFollowerContext(DbContextOptions<UserFollowerContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
