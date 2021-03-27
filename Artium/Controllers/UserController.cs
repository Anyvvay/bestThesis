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
        IWebHostEnvironment _appEnvironment;
        public UserController(UserContext user_context, PicUserContext userpics_context, IWebHostEnvironment appEnvironment, WallPostContext post_context)
        {
            db_user = user_context;
            db_userpic = userpics_context;
            db_post = post_context;
            _appEnvironment = appEnvironment;
        }
        [Route("{login}")]
        public async Task<IActionResult> Index(string login)
        {
            if (login != null)
            {
                User user = await db_user.Users.FirstOrDefaultAsync(u => u.Login == login);

                if (user != null)
                {
                    if (User.Identity.Name == user.Login)
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

                    ViewBag.login = user.Login;

                    ViewBag.WallPosts = db_post.WallPosts.Where(u => u.UserId == user.Id);
                    ViewBag.Users = db_post.Users.ToListAsync();

                    UserInfo userInfo = await db_user.UserInfos.FirstOrDefaultAsync(i => i.User == user);
                    ViewBag.description = userInfo.Description;
                    ViewBag.name = userInfo.Name;

                    Userpic userpic = await db_userpic.Userpics.FirstOrDefaultAsync(up => up.User == user);

                    if (userpic != null)
                    {
                        ViewBag.userpic = userpic.Path;
                    }
                    else
                    {
                        Userpic defaultPic = await db_userpic.Userpics.FirstOrDefaultAsync(d => d.Id == 1);
                        ViewBag.userpic = defaultPic.Path;
                    }

                    Bguserpic bguserpic = await db_userpic.Bguserpics.FirstOrDefaultAsync(up => up.User == user);
                    if (bguserpic != null)
                    {
                        ViewBag.bguserpic = bguserpic.Path;
                    }
                    else
                    {
                        Bguserpic defaultBgPic = await db_userpic.Bguserpics.FirstOrDefaultAsync(d => d.Id == 1);
                        ViewBag.bguserpic = defaultBgPic.Path;
                    }
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
            User user = await db_user.Users.FirstOrDefaultAsync(u => u.Login == User.Identity.Name);

            if (bgUserpic != null)
            {
                // путь к папке Files
                string path = "/user_img/bguserpic/" + bgUserpic.FileName;
                // сохраняем файл 

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await bgUserpic.CopyToAsync(fileStream);
                }

                Bguserpic prevBgUserpic = await db_userpic.Bguserpics.FirstOrDefaultAsync(u => u.UserId == user.Id);
                if (prevBgUserpic != null)
                {
                    db_userpic.Bguserpics.Remove(prevBgUserpic);
                }

                Bguserpic newBgUserpic = new Bguserpic { Path = path, UserId = user.Id };
                db_userpic.Bguserpics.Add(newBgUserpic);
                db_userpic.SaveChanges();
            }

            if (userpic != null)
            {
                // путь к папке Files
                string path = "/user_img/userpic/" + userpic.FileName;
                // сохраняем файл 

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await userpic.CopyToAsync(fileStream);
                }

                Userpic prevUserpic = await db_userpic.Userpics.FirstOrDefaultAsync(u => u.UserId == user.Id);
                if (prevUserpic != null)
                {
                    db_userpic.Userpics.Remove(prevUserpic);
                }

                Userpic newUserpic = new Userpic { Path = path, UserId = user.Id };
                db_userpic.Userpics.Add(newUserpic);
                db_userpic.SaveChanges();
            }



            UserInfo userInfo = await db_user.UserInfos.FirstOrDefaultAsync(u => u.UserId == user.Id);
            Console.WriteLine(userInfo.Name);
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
                    WallPost wallPost = new WallPost { Text = text, UserId = user.Id, Date = DateTime.Now};
                    db_post.WallPosts.Add(wallPost);
                    db_post.SaveChanges();
                };
            }
            return LocalRedirect("~/" + User.Identity.Name);
        }
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            Console.WriteLine(id);
            WallPost post = await db_post.WallPosts.FirstOrDefaultAsync(u => u.Id == id);

            if (post != null)
            {
                db_post.WallPosts.Remove(post);
                db_post.SaveChanges();
            }
            return LocalRedirect("~/" + User.Identity.Name);
        }
    }
}
