using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artium.Models.Objects
{
    public class PostDislike
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int WallPostId { get; set; }
        public WallPost WallPost { get; set; }
    }
}
