using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TeamService.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<IdentityUser> signInManager;
        public IActionResult Login(string returnUrl = "/")
        {
            return new ChallengeResult("Auth0", new AuthenticationProperties() { RedirectUri = returnUrl });
        }
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("Auth0");
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");

        }

        [Authorize]
        public IActionResult Claims()
        {
            ViewData["Title"] = "Claims";
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ViewData["picture"] = identity.FindFirst("picture").Value;
            return View();
        }
    }
}
