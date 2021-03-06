﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godius.Shop.Data;
using Godius.Shop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Godius.Shop.Controllers
{
	[Authorize]
	public class ShopController : Controller
    {
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IHostingEnvironment _hostingEnvironment;
		private readonly AppOptions _appOptions;

		public ShopController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
			IHostingEnvironment hostingEnvironment, IOptions<AppOptions> appOptionsAccessor)
		{
			_context = context;
			_userManager = userManager;
			_hostingEnvironment = hostingEnvironment;
			_appOptions = appOptionsAccessor.Value;
		}
		
		// GET: Shop/Index
		[AllowAnonymous]
		public async Task<IActionResult> Index()
        {
			var goods = await _context.Goods.OrderBy(G => G.SerialCode).ToListAsync();
            return View(goods);
        }

		// GET: Shop/Buy/5
		public async Task<IActionResult> Buy(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var goods = await _context.Goods.Include(G => G.ItemsGoods)
											.ThenInclude(IG => IG.Item)
											.SingleOrDefaultAsync(m => m.Id == id);
			if (goods == null)
			{
				return NotFound();
			}

			return View(goods);
		}

		// POST: Shop/Buy/5
		[HttpPost, ActionName("Buy")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ResultItemGoods(Guid id)
		{
			if (ModelState.IsValid)
			{
				if (id == null)
				{
					return NotFound();
				}

				var goods = await _context.Goods.Include(G => G.ItemsGoods).SingleOrDefaultAsync(m => m.Id == id);
				if (goods == null)
				{
					return NotFound();
				}

				// new Purchase
				var applicationUser = await _userManager.GetUserAsync(User);
				var purchase = new Purchase
				{
					Goods = goods,
					Purchaser = applicationUser,
					Date = DateTime.Now,
				};
				_context.Add(purchase);

				// select ItemGoods
				var source = new List<ItemGoods>();
				foreach (var itemGoods in goods.ItemsGoods)
				{
					var count = itemGoods.Probability * 100;
					for (int i = 0; i < count; i++)
					{
						source.Add(itemGoods);
					}
				}

				var selectedItemGoods = source.OrderBy(I => Guid.NewGuid()).FirstOrDefault();
				await _context.Entry(selectedItemGoods).Reference(IG => IG.Item).LoadAsync();

				// create ResultItemGoods
				var resultItemGoods = new ResultItemGoods
				{
					ItemGoods = selectedItemGoods,
					Purchase = purchase
				};
				// calculate Option
				if (selectedItemGoods.NeedAdditionalUpgrade)
				{
					var currentTick = DateTime.Now.Ticks;
					var isUpgradeOption = currentTick % 2 == 0;
					var upgradeOption = (int)(currentTick % 4);
					if (isUpgradeOption)
					{// WC or AC
						resultItemGoods.UpgradeOption = upgradeOption;
					}
					else
					{// Durability
						resultItemGoods.UpgradeDurability = upgradeOption;
					}
				}

				_context.Add(resultItemGoods);
				
				await _context.SaveChangesAsync();

				return View("ResultItemGoods", resultItemGoods);
			}

			return View(nameof(Buy), new { id = id });
		}
	}
}