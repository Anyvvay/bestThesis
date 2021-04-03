using Artium.Models;
using Artium.Models.Contexts;
using Artium.Models.Objects;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Artium.Controllers
{
    public class PostController : Controller
    {
        private UserContext db_user;
        private PicUserContext db_userpic;
        private WallPostContext db_post;
        IWebHostEnvironment _appEnvironment;
        public PostController(UserContext user_context, PicUserContext userpics_context, IWebHostEnvironment appEnvironment, WallPostContext post_context)
        {
            db_user = user_context;
            db_userpic = userpics_context;
            db_post = post_context;
            _appEnvironment = appEnvironment;
        }

        [Route("{Controller=Post}/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            WallPost wallPost = await db_post.WallPosts.FirstOrDefaultAsync(p => p.Id == id);
            if (wallPost != null)
            {
                User author = await db_user.Users.FirstOrDefaultAsync(p => p.Id == wallPost.UserId);
                User user = await db_user.Users.FirstOrDefaultAsync(p => p.Login == User.Identity.Name);

                ViewBag.Date = wallPost.Date;
                ViewBag.Text = wallPost.Text;
                ViewBag.Author = author;
                ViewBag.User = user;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewComment(string text, int WallPostId)
        {
            User user = await db_user.Users.FirstOrDefaultAsync(u => u.Login == User.Identity.Name);

            if (user != null)
            {
                if (text != null)
                {
                    WallPost wallPost = await db_post.WallPosts.FirstOrDefaultAsync(u => u.Id == WallPostId);
                    Comment comment = new Comment { Text = text, UserId = user.Id, Date = DateTime.Now, WallPostId =  wallPost.Id};

                    wallPost.Comments += 1;
                    db_post.WallPosts.Update(wallPost);
                    db_post.Comments.Add(comment);
                    db_post.SaveChanges();

                    return LocalRedirect("~/" + wallPost.User);
                };
            }
            return LocalRedirect("~/" + User.Identity.Name);
        }
    }
}
