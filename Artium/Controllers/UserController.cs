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
    public class UserController : Controller
    {
        private UserContext db_user;
        private PicUserContext db_userpic;
        private WallPostContext db_post;
        private UserFollowerContext db_follower;
        IWebHostEnvironment _appEnvironment;
        public UserController(
            UserContext user_context,
            PicUserContext userpics_context,
            IWebHostEnvironment appEnvironment,
            WallPostContext post_context,
            UserFollowerContext follower_context
            )
        {
            db_user = user_context;
            db_userpic = userpics_context;
            db_post = post_context;
            db_follower = follower_context;
            _appEnvironment = appEnvironment;
        }

        [Route("{login}")]
        public async Task<IActionResult> Index(string login)
        {
            if (login != null)
            {
                User userOwner = await db_user.Users
                        .Include(u => u.UserInfo)
                        .Include(u => u.UserInfo.Bguserpic)
                        .Include(u => u.UserInfo.Userpic)
                        .FirstOrDefaultAsync(u => u.Login == login);
                User user = await db_user.Users
                        .Include(u => u.UserInfo)
                        .Include(u => u.UserInfo.Bguserpic)
                        .Include(u => u.UserInfo.Userpic).FirstOrDefaultAsync(u => u.Login == User.Identity.Name);

                UserFollower Follower = await db_follower.Followers
                        .Include(u => u.User)
                        .Include(u => u.User.UserInfo)
                        .Include(u => u.User.UserInfo.Userpic).FirstOrDefaultAsync(f => f.FollowerId == user.Id && f.User.Id == userOwner.Id);

                if (Follower != null)
                {
                    ViewBag.isFollower = true;
                } else
                {
                    ViewBag.isFollower = false;
                }

                if (userOwner != null)
                {
                    if (User.Identity.Name == userOwner.Login)
                    {
                        ViewBag.owner = true;
                    }
                    else
                    {
                        ViewBag.owner = false;
                    }

                    if (User.Identity.IsAuthenticated)
                    {
                        ViewBag.isAuth = true;
                    }
                    else
                    {
                        ViewBag.isAuth = false;
                    }

                    ViewBag.ownerLogin = userOwner.Login;

                    ViewBag.WallPosts = db_post.WallPosts
                        .Where(u => u.UserId == userOwner.Id)
                        .Where(u => u.Disabled == 0)
                        .Include(u => u.User)
                        .Include(u => u.User.UserInfo)
                        .Include(u => u.User.UserInfo.Bguserpic)
                        .Include(u => u.User.UserInfo.Userpic);
                    ViewBag.Users = db_user.Users.ToListAsync();
                    
                    ViewBag.pageOwner = userOwner;
                    ViewBag.user = user;
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(IFormFile bgUserpic, IFormFile userpic, string name, string description)
        {
            User userOwner = await db_user.Users
                        .Include(u => u.UserInfo)
                        .Include(u => u.UserInfo.Bguserpic)
                        .Include(u => u.UserInfo.Userpic)
                        .FirstOrDefaultAsync(u => u.Login == User.Identity.Name);

            if (bgUserpic != null)
            {
                // путь к папке Files
                string path = "/user_img/bguserpic/" + bgUserpic.FileName;
                // сохраняем файл 

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await bgUserpic.CopyToAsync(fileStream);
                }

                Bguserpic newBgUserpic = new Bguserpic { Path = path };
                Bguserpic prevBgUserpic = userOwner.UserInfo.Bguserpic;
                db_userpic.Bguserpics.Add(newBgUserpic);
                db_userpic.SaveChanges();

                userOwner.UserInfo.BguserpicId = newBgUserpic.Id;
                db_user.UserInfos.Update(userOwner.UserInfo);
                db_user.SaveChanges();

                if (prevBgUserpic != null)
                {
                    if (prevBgUserpic.Id != 1)
                    {
                        db_userpic.Bguserpics.Remove(prevBgUserpic);
                        db_userpic.SaveChanges();
                    }
                }

                db_userpic.Bguserpics.Add(newBgUserpic);          
            }

            if (userpic != null)
            {
                // путь к папке Files
                string path = "/user_img/userpic/" + userpic.FileName;
                // сохраняем файл 

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await bgUserpic.CopyToAsync(fileStream);
                }

                Userpic newUserpic = new Userpic { Path = path };
                Userpic prevUserpic = userOwner.UserInfo.Userpic;
                db_userpic.Userpics.Add(newUserpic);
                db_userpic.SaveChanges();

                userOwner.UserInfo.UserpicId = newUserpic.Id;
                db_user.UserInfos.Update(userOwner.UserInfo);
                db_user.SaveChanges();

                if (prevUserpic != null)
                {
                    if (prevUserpic.Id != 1)
                    {
                        db_userpic.Userpics.Remove(prevUserpic);
                        db_userpic.SaveChanges();
                    }
                }

                db_userpic.Userpics.Add(newUserpic);
            }

            UserInfo userInfo = await db_user.UserInfos.FirstOrDefaultAsync(u => u.Id == userOwner.UserInfoId);

            if (userInfo != null)
            {
                if (name != null)
                {
                    userInfo.Name = name;
                    db_user.UserInfos.Update(userInfo);
                    db_user.SaveChanges();
                }
                if (description != null)
                {
                    userInfo.Description = description;
                    db_user.UserInfos.Update(userInfo);
                    db_user.SaveChanges();
                }
            }

            return LocalRedirect("~/" + User.Identity.Name);
        }

        [HttpPost]
        public async Task<IActionResult> NewPost(string text)
        {
            User user = await db_user.Users.FirstOrDefaultAsync(u => u.Login == User.Identity.Name);
            
            if (user != null)
            {
                if (text != null)
                { 
                    WallPost wallPost = new WallPost { Text = text, UserId = user.Id, Date = DateTime.Now, Likes = 0, Dislikes = 0, Comments = 0, Disabled = 0 };
                    db_post.WallPosts.Add(wallPost);
                    db_post.SaveChanges();
                };
            }
            return LocalRedirect("~/" + User.Identity.Name);
        }
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            WallPost post = await db_post.WallPosts.FirstOrDefaultAsync(u => u.Id == id);

            if (post != null)
            {
                post.Disabled = 1;
                db_post.WallPosts.Update(post);
                db_post.SaveChanges();
            }
            return LocalRedirect("~/" + User.Identity.Name);
        }
        [HttpPost]
        public async Task<ActionResult> Follow(int userId, int followerId)
        {
            User user = await db_user.Users
                         .Include(u => u.UserInfo)
                         .Include(u => u.UserInfo.Bguserpic)
                         .Include(u => u.UserInfo.Userpic)
                         .FirstOrDefaultAsync(u => u.Id == userId);
            User follower = await db_user.Users
                    .Include(u => u.UserInfo)
                    .Include(u => u.UserInfo.Bguserpic)
                    .Include(u => u.UserInfo.Userpic).FirstOrDefaultAsync(u => u.Id == followerId);

            follower.UserInfo.Followers += 1;

            UserFollower Follower = new UserFollower { FollowerId = follower.Id, UserID = user.Id };

            db_follower.Followers.Add(Follower);
            db_user.UserInfos.Update(follower.UserInfo);
            db_follower.SaveChanges();
            db_user.SaveChanges();

            return LocalRedirect("~/" + User.Identity.Name);
        }
        [HttpPost]
        public async Task<ActionResult> Unfollow(int userId, int followerId)
        {
            User user = await db_user.Users
                         .Include(u => u.UserInfo)
                         .Include(u => u.UserInfo.Bguserpic)
                         .Include(u => u.UserInfo.Userpic)
                         .FirstOrDefaultAsync(u => u.Id == userId);
            User follower = await db_user.Users
                    .Include(u => u.UserInfo)
                    .Include(u => u.UserInfo.Bguserpic)
                    .Include(u => u.UserInfo.Userpic).FirstOrDefaultAsync(u => u.Id == followerId);

            follower.UserInfo.Followers -= 1;

            UserFollower Follower = new UserFollower { FollowerId = follower.Id, UserID = user.Id };

            db_follower.Followers.Remove(Follower);
            db_user.UserInfos.Update(follower.UserInfo);
            db_follower.SaveChanges();
            db_user.SaveChanges();

            return LocalRedirect("~/" + User.Identity.Name);
        }

    }
}
