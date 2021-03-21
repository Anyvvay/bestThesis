using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Artium.Models
{
    public class RoleContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public RoleContext(DbContextOptions<RoleContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
