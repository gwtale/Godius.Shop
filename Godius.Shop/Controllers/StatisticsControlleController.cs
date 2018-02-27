using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Godius.Shop.Data;
using Godius.Shop.Extensions;
using Godius.Shop.Models;
using Godius.Shop.Models.StatisticsViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Godius.Shop.Controllers
{
	public class StatisticsController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public StatisticsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			return View();
		}

		public async Task<IActionResult> PerItemStatistics()
		{
			var purchases = await _context.PurchaseHistory.Include(P => P.Goods)
														.Include(P => P.ResultItemGoods)
														.ThenInclude(RIG => RIG.ItemGoods)
														.ThenInclude(IG => IG.Item)
														.ToListAsync();

			var result = from purchase in purchases
						 group purchase by purchase.Goods into goodsGroup
						 orderby goodsGroup.Key.SerialCode
						 select goodsGroup;

			return PartialView("_PerItemStatistics", result);
		}

		public async Task<IActionResult> PerUserStatistics()
		{
			// RichUser
			var richUsers = await (from user in _context.Users
								   .Include(U => U.Purchases)
								   .ThenInclude(P => P.Goods)
								   let TotalPurchasePrice = user.Purchases.Sum(P => P.Goods.Price)
								   orderby TotalPurchasePrice descending
								   select new
								   {
									   UserId = user.Id,
									   UserEmail = BoardHelper.GetHidedUserEmail(user.Email),
									   TotalPurchasePrice
								   }.ToExpando())
								   .Take(5).ToListAsync();

			// LuckyUser
			var luckyUsers = await (from user in _context.Users
								    .Include(U => U.Purchases)
								    .ThenInclude(P => P.ResultItemGoods)
								    .ThenInclude(RIG => RIG.ItemGoods)
								    .ThenInclude(IG => IG.Item)
								    let Lucky = user.Purchases.Sum(P => P.ResultItemGoods.ItemGoods.Probability) / user.Purchases.Count
								    where user.Purchases.Count > 0
								    orderby Lucky
									select new
									{
										UserId = user.Id,
										UserEmail = BoardHelper.GetHidedUserEmail(user.Email),
										Lucky,
										TotalPurchaseCount = user.Purchases.Count,
										Items = user.Purchases.Select(P => P.ResultItemGoods.ItemGoods).OrderBy(IG => IG.Probability).Take(5).Select(IG => IG.Item).ToList()
									}.ToExpando())
									.Take(5).ToListAsync();

			return PartialView("_PerUserStatistics", new PerUserStatisticsViewModel { RichUsers = richUsers, LuckyUsers = luckyUsers });
		}
	}
}