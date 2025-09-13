using Microsoft.AspNetCore.Mvc;
using MoodPlaylistGenerator.Services;
using MoodPlaylistGenerator.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using MoodPlaylistGenerator.Models;

namespace MoodPlaylistGenerator.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var (user, error) = await _authService.RegisterUserAsync(model.Username, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", error ?? "An unknown error occurred.");
                return View(model);
            }

            await SignInUser(user.Username, user.Id);
            return RedirectToAction("Dashboard", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The line causing the error is below
            (User? user, string? error) = await _authService.SignInUserAsync(model.Username, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", error ?? "Invalid login attempt.");
                return View(model);
            }

            await SignInUser(user.Username, user.Id);
            return RedirectToAction("Dashboard", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(string username, int userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Remember me
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}