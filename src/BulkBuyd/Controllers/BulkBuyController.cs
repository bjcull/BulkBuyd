using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkBuyd.Data;
using BulkBuyd.Domain.Entities;
using BulkBuyd.Domain.Other;
using BulkBuyd.Domain.Services;
using BulkBuyd.Models.BulkBuy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkBuyd.Controllers
{
    [Authorize]
    public class BulkBuyController : Controller
    {
        private readonly BulkBuydContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IShortcodeService _shortCodeService;

        public BulkBuyController(BulkBuydContext context, UserManager<User> userManager, IShortcodeService shortCodeService, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _shortCodeService = shortCodeService;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Details(string id)
        {
            var buy = _context.BulkBuys
                .Include(x => x.Pledges)
                .ThenInclude(x => x.User)
                .Include(x => x.Owner)
                .FirstOrDefault(x =>
                    x.DisplayId == id
                    && !x.IsDeleted);

            if (buy == null)
            {
                return NotFound();
            }

            var model = ViewVm.FromEntity(buy);
            model.IsLoggedIn = _signInManager.IsSignedIn(User);
            model.RedirectUrl = Url.Action("Details", "BulkBuy", new {id = model.Id});

            if (model.IsLoggedIn)
            {
                var userId = _userManager.GetUserId(User);
                var pledge = _context.Pledges
                    .FirstOrDefault(x => x.BulkBuy.DisplayId == model.Id
                                         && x.UserId == userId
                                         && !x.IsDeleted);

                if (pledge != null)
                {
                    model.IsPledged = true;
                    model.PledgeAmount = pledge.PledgeAmount;
                    model.OrderDetails = pledge.OrderDetails;
                }
            }

            return View(model);
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New(CreateVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var bulkBuy = new BulkBuy()
            {
                DisplayId = _shortCodeService.GenerateId(IdPrefix.BULK_BUY),
                Name = model.Name,
                Description = model.Description,
                DueDate = model.ClosingDate,
                OwnerId = _userManager.GetUserId(User),
                CreatedDate = DateTime.UtcNow
            };
            _context.BulkBuys.Add(bulkBuy);
            _context.SaveChanges();

            return RedirectToAction("Details", new {id = bulkBuy.DisplayId});
        }

        public IActionResult Admin(string id)
        {
            var userId = _userManager.GetUserId(User);

            var buy = _context.BulkBuys
                .Include(x => x.Owner)
                .FirstOrDefault(x =>
                    x.DisplayId == id
                    && !x.IsDeleted
                    && x.OwnerId == userId);

            if (buy == null)
            {
                return NotFound();
            }

            var model = AdminVm.FromEntity(buy);

            return View(model);
        }

        [HttpPost]
        public IActionResult Admin(AdminVm model)
        {
            var userId = _userManager.GetUserId(User);

            var buy = _context.BulkBuys
                .Include(x => x.Owner)
                .FirstOrDefault(x =>
                    x.DisplayId == model.Id
                    && !x.IsDeleted
                    && x.OwnerId == userId);

            if (buy == null)
            {
                return NotFound();
            }

            buy.Name = model.Name;
            buy.Description = model.Description;
            buy.DueDate = model.ClosingDate;

            _context.SaveChanges();

            return View(model);
        }
    }
}