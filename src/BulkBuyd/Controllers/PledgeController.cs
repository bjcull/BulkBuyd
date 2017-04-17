using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkBuyd.Data;
using BulkBuyd.Domain.Entities;
using BulkBuyd.Models.Pledges;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BulkBuyd.Controllers
{
    [Authorize]
    public class PledgeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly BulkBuydContext _context;

        public PledgeController(UserManager<User> userManager, BulkBuydContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Save(AddPledgeVm model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "BulkBuy", new { id = model.Id });
            }

            var bulkBuy = _context.BulkBuys
                .FirstOrDefault(x => x.DisplayId == model.Id
                                     && !x.IsDeleted);

            if (bulkBuy == null)
            {
                ModelState.AddModelError("", $"Could not find a Bulk Buy with ID: {model.Id}");
                return RedirectToAction("Details", "BulkBuy", new { id = model.Id });
            }

            var userId = _userManager.GetUserId(User);
            var existing = _context.Pledges
                .FirstOrDefault(x => x.UserId == userId
                                     && x.BulkBuy.DisplayId == model.Id
                                     && !x.IsDeleted);

            if (existing != null)
            {
                existing.OrderDetails = model.OrderDetails;
                existing.PledgeAmount = model.PledgeAmount;
                _context.SaveChanges();
            }
            else
            {
                var pledge = new Pledge()
                {
                    UserId = userId,
                    BulkBuyId = bulkBuy.Id,
                    CreatedDate = DateTime.UtcNow,
                    OrderDetails = model.OrderDetails,
                    PledgeAmount = model.PledgeAmount
                };
                _context.Pledges.Add(pledge);
                _context.SaveChanges();
            }

            return RedirectToAction("Details", "BulkBuy", new { id = bulkBuy.DisplayId });
        }
    }
}
