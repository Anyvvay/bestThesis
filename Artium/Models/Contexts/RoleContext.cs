using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Artium.Models.Objects;
using Microsoft.EntityFrameworkCore;

namespace Artium.Models.Contexts
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
