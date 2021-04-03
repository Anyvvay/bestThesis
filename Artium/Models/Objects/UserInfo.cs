using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artium.Models.Objects
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Followers { get; set; }
        public int UserpicId { get; set; }
        public Userpic? Userpic { get; set; }
        public int BguserpicId { get; set; }
        public Bguserpic? Bguserpic { get; set; }
    }
}
