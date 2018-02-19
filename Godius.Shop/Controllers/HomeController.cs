using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Godius.Shop.Models;
using Godius.Shop.Data;
using Microsoft.EntityFrameworkCore;

namespace Godius.Shop.Controllers
{
    public class HomeController : Controller
    {
		private readonly ApplicationDbContext _context;

		public HomeController(ApplicationDbContext context)
		{
			_context = context;
		}

        public async Task<IActionResult> Index()
        {
			var goods = await _context.Goods.OrderBy(G => G.SerialCode).ToListAsync();
			return View(goods);
		}

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
