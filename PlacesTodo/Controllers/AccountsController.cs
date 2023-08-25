using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlacesTodo.Context;
using PlacesTodo.Models;
using System.Security.Claims;

namespace PlacesTodo.Controllers
{
    public class AccountsController : Controller
    {
        protected readonly AppDBContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountsController(AppDBContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.Name!, loginModel.Password!, loginModel.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToRoute(returnUrl);
                }
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
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                Folder userRoot = new Folder() { Name = registerModel.Name + "_root" };
                _context.Folders.Add(userRoot);
                await _context.SaveChangesAsync();
                User user = new User() { UserName = registerModel.Name, FolderId = userRoot.Id};
                var result = await _userManager.CreateAsync(user, registerModel.Password!);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}
