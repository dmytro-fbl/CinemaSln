using CinemaBookingSystem.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBookingSystem.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signMgr)
        {
            userManager = userMgr;
            signInManager = signMgr;
        }

        public ViewResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = model.Name,
                    Email = model.Email,
                };

                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        public ViewResult Login( string? returnUrl)
        {
            return View(new LoginModel
            {
                Name = string.Empty,
                Password = string.Empty,
                ReturnUrl = returnUrl ?? "/"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser? user = await userManager.FindByNameAsync(loginModel.Name);

                if (user != null)
                {
                    await signInManager.SignOutAsync();

                    if((await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/Admin");
                    }
                }
                ModelState.AddModelError("", "Невірне ім'я або пароль");
            }
            return View(loginModel);

        }

        [Authorize]
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(RegisterModel model)
        {
            IdentityUser? user = await userManager.FindByNameAsync(User.Identity?.Name ?? "");

            if (user != null)
            {
                user.UserName = model.Name;
                user.Email = model.Email;

                IdentityResult result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    await signInManager.RefreshSignInAsync(user);

                    TempData["Message"] = "Дані успішно оновлено!";
                    return RedirectToAction("Profile");
                }
                else
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult>  Profile()
        {
            string? userName = User.Identity?.Name;

            if (userName == null) return RedirectToAction("Login");

            IdentityUser? user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return NotFound();
            }

            var model = new RegisterModel
            {
                Name = user.UserName ?? "",
                Email = user.Email ?? ""
            };

            return View(model);
        }
       
    }
}
