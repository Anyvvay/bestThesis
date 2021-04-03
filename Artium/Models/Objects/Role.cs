using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artium.Models.Objects
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Role(string name, int id)
        {
            Name = name;
            Id = id;
        }
    }
}