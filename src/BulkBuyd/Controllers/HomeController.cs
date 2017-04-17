using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkBuyd.Data;
using BulkBuyd.Domain.DTOs;
using BulkBuyd.Models.Home;
using Microsoft.AspNetCore.Mvc;
using Remotion.Linq.Clauses;

namespace BulkBuyd.Controllers
{
    public class HomeController : Controller
    {
        private readonly BulkBuydContext _context;

        public HomeController(BulkBuydContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new IndexVm();

            var buys = _context.BulkBuys
                .OrderByDescending(x => x.CreatedDate)
                .Select(BulkBuyDto.Projection)
                .ToList();

            model.BulkBuys = buys
                .AsQueryable()
                .Select(BulkBuyVm.Projection)
                .ToList();

            return View(model);
        }
    }
}
