using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artium.Models.Objects
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public int? WallPostId { get; set; }
        public WallPost? WallPost { get; set; }

        public string GetUserLogin()
        {
            return User.Login;
        }
    }
}
