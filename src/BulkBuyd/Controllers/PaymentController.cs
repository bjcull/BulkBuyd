using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkBuyd.Data;
using BulkBuyd.Domain.Entities;
using BulkBuyd.Models.Payments;
using BulkBuyd.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pinch.SDK;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BulkBuyd.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly BulkBuydContext _context;
        private readonly UserManager<User> _userManager;

        public PaymentController(IOptions<AppSettings> appSettings, BulkBuydContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        public async Task<IActionResult> Method()
        {
            var model = new MethodVm();
            var api = GetApi();

            var user = await _userManager.GetUserAsync(User);

            if (!string.IsNullOrEmpty(user.PinchPayerId))
            {
                model.HasPayerAccount = true;

                var payer = await api.Payer.Get(user.PinchPayerId);

                if (!payer.Success)
                {
                    ModelState.AddModelError("", string.Join(" - ", payer.Errors.SelectMany(x => x.ErrorMessage)));
                    return View(model);
                }

                model.AccountName = payer.Data.AccountName;
                model.AccountBsb = payer.Data.BSB;
                model.AccountNumber = payer.Data.AccountNumber;                
            }

            model.PinchUrl = $"https://web.getpinch.com.au/debit/{_appSettings.Pinch.MerchantId}/";

            model.PinchUrl += $"?redirectUrl={Url.Action("Return", "Payment", null, Request.Scheme)}&nonce={Guid.NewGuid()}";

            if (!_appSettings.Pinch.IsLive)
            {
                model.PinchUrl += "&test=true";
            }

            return View(model);
        }

        public async Task<IActionResult> Return(string payerId)
        {
            var user = await _userManager.GetUserAsync(User);
            user.PinchPayerId = payerId;
            _context.SaveChanges();
            
            return RedirectToAction("Method");
        }

        [HttpPost]
        public async Task<IActionResult> Remove()
        {
            var user = await _userManager.GetUserAsync(User);

            if (!string.IsNullOrEmpty(user.PinchPayerId))
            {
                await GetApi().Payer.Delete(user.PinchPayerId);
                user.PinchPayerId = null;
                _context.SaveChanges();
            }

            return RedirectToAction("Method");
        }

        private PinchApi GetApi()
        {
            return new PinchApi(
                _appSettings.Pinch.MerchantId,
                _appSettings.Pinch.SecretKey,
                new PinchApiOptions()
                {
                    IsLive = _appSettings.Pinch.IsLive
                });
        }
    }
}
