using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkBuyd.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BulkBuyd.ViewComponents
{
    public class LoginNavBarViewComponent : ViewComponent
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public LoginNavBarViewComponent(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (!_signInManager.IsSignedIn(HttpContext.User))
            {
                return View();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            return View("LoggedIn", user);
        }
    }
}
