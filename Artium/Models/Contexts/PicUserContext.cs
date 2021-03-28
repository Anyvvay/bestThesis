using Artium.Models.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artium.Models.Contexts
{
    public class PicUserContext : DbContext 
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Userpic> Userpics { get; set; }
        public DbSet<Bguserpic> Bguserpics { get; set; }
        public PicUserContext(DbContextOptions<PicUserContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

    