﻿
namespace RestaurantOnlineBookingApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using RestaurantOnlineBookingApp.Data.Models;
    using RestaurantOnlineBookingApp.Web.ViewModels.User;
    using static RestaurantOnlineBookingApp.Common.NotificationMessages;
    using static Common.ApplicationConstants;

    public class UserController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly IMemoryCache memoryCache;
        public UserController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
            IMemoryCache memoryCache)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]        
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AppUser user = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            await userManager.SetEmailAsync(user, model.Email);
            await userManager.SetUserNameAsync(user, model.Email);

            IdentityResult result =
                await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }

            await signInManager.SignInAsync(user, false);
            this.memoryCache.Remove(UsersCacheKey);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            //clear the cookies
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            LoginFormModel model = new LoginFormModel()
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Log or display a message indicating that the user does not exist
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(model);
            }

            var result =
                await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                TempData[ErrorMessage] =
                    "There was an error while logging you in! Please try again later or contact an administrator.";

                return View(model);
            }

            return Redirect(model.ReturnUrl ?? "/Home/Index");
        }
       
    }
}
