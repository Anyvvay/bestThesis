using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Artium.AuthModels; // пространство имен моделей RegisterModel и LoginModel
using Artium.Models; // пространство имен UserContext и класса User
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Artium.Models.Objects;
using Artium.Models.Contexts;
using System;

namespace AuthApp.Controllers
{
    public class AccountController : Controller
    {
        private UserContext db;
        public AccountController(UserContext userContext)
        {
            db = userContext;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                //пытаемся войти по почте
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.inputData && u.Password == model.Password);
                
                if (user == null)
                {
                    //тогда входим по логину
                    user = await db.Users.FirstOrDefaultAsync(u => u.Login == model.inputData && u.Password == model.Password);
                }
                if (user != null)
                {
                    await Authenticate(user.Login); // аутентификация
                    return LocalRedirect("~/" + user.Login);
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд 
                    Random rnd = new Random();
                    UserInfo userInfo = new UserInfo { Name = model.Name, Description = "", BguserpicId = rnd.Next(1, 6), UserpicId = rnd.Next(1, 6), Followers = 0 };
                    db.UserInfos.Add(userInfo);
                    await db.SaveChangesAsync();
                    User newUser = new User { Login = model.Login, Email = model.Email, Password = model.Password, UserInfoId = userInfo.Id};
                    db.Users.Add(newUser);
                    await db.SaveChangesAsync();

                    await Authenticate(model.Email); // аутентификация

                    return LocalRedirect("~/" + newUser.Login);
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}