using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkBuyd.Data;
using BulkBuyd.Domain.Entities;
using BulkBuyd.Models.Organiser;
using BulkBuyd.Services.Organiser;
using BulkBuyd.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BulkBuyd.Controllers
{
    [Authorize]
    public class OrganiserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly BulkBuydContext _context;
        private readonly OrganiserService _organiserService;
        private readonly AppSettings _appSettings;

        public OrganiserController(UserManager<User> userManager, BulkBuydContext context, OrganiserService organiserService, IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _context = context;
            _organiserService = organiserService;
            _appSettings = appSettings.Value;
        }

        public async Task<IActionResult> Account()
        {
            var user = await _userManager.GetUserAsync(User);

            var model = new AccountVm()
            {
                StreetAddress = user.StreetAddress,
                MobileNumber = user.PhoneNumber,
                Postcode = user.Postcode,
                Suburb = user.Suburb
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Account(AccountVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            user.StreetAddress = model.StreetAddress;
            user.Postcode = model.Postcode;
            user.Suburb = model.Suburb;
            user.PhoneNumber = model.MobileNumber;

            _context.SaveChanges();
            
            var merchant = await _organiserService.GetMerchant(user.Id, _appSettings.Pinch.IsLive);

            if (merchant != null)
            {
                var response = await _organiserService.UpdateMerchant(user.Id, _appSettings.Pinch.IsLive,
                    _appSettings.Pinch.MerchantId, _appSettings.Pinch.SecretKey);

                if (!response.Successful)
                {
                    ModelState.AddModelError("", string.Join(" - ", response.ErrorMessages.Select(x => x.ErrorMessage)));
                    return View(model);
                }
            }
            else
            {
                var response = await _organiserService.CreateMerchant(user.Id, _appSettings.Pinch.IsLive,
                    _appSettings.Pinch.MerchantId, _appSettings.Pinch.SecretKey);

                if (!response.Successful)
                {
                    ModelState.AddModelError("", string.Join(" - ", response.ErrorMessages.Select(x => x.ErrorMessage)));
                    return View(model);
                }
            }

            return RedirectToAction("Account");
        }
    }
}
