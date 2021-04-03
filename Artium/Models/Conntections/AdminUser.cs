using Artium.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artium.Models
{
    public class AdminUser
    {
        public int Id { get; set; }
        public User User { get; set; }
    }
}
