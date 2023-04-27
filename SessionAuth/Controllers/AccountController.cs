using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SessionAuth.Model.Models;
using SessionAuth.DataAccess.Data;

namespace SessionAuth.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(string returnUrl = "/")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password, string returnUrl = "/")
        {
            if (email == "test@mail.com" && password == "password")
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "user")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            HttpContext.Session.SetString("UserName", "test@mail.com");
            var user = new UserInfo { Email = email };
            HttpContext.Session.SetObject("User", user);
            HttpContext.Session.SetString("SessionCreatedTime", DateTime.Now.ToString());
                return Json(new {returnUrl, email});
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        //[HttpPost]
        public IActionResult Logout()
        
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // remove a session variable by key
            //HttpContext.Session.Remove("User");

            // clear all session variables
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
