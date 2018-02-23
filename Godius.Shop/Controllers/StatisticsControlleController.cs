using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godius.Shop.Data;
using Godius.Shop.Models;
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
			var purchases = await _context.PurchaseHistory.Include(P => P.Goods)
														.Include(P => P.ResultItemGoods)
														.ThenInclude(RIG => RIG.ItemGoods)
														.ThenInclude(IG => IG.Item)
														.ToListAsync();

			var result = from purchase in purchases
						 group purchase by purchase.Goods into goodsGroup
						 orderby goodsGroup.Key.SerialCode
						 select goodsGroup;

			//foreach (var goodsGroup in result)
			//{
			//	var goodsItem = goodsGroup.Key;
			//	System.Diagnostics.Debug.WriteLine($"Goods.Name = {goodsItem.Name}");
			//	System.Diagnostics.Debug.WriteLine($"Goods.TotalPurchaseCount = {goodsItem.PurchaseHistory.Count}");

			//	foreach (var itemsGoodsItem in goodsItem.ItemsGoods.OrderBy(IG => IG.Probability))
			//	{
			//		System.Diagnostics.Debug.WriteLine($"\tItem.Name = {itemsGoodsItem.Item.Name}");
			//		System.Diagnostics.Debug.WriteLine($"\tItem.Probability = {itemsGoodsItem.Probability}%");
			//		System.Diagnostics.Debug.WriteLine($"\tItem.ResultCount = {itemsGoodsItem.ResultItemGoods.Count}");
			//		var actualProbability = 0d;
			//		if (goodsItem.PurchaseHistory.Count > 0)
			//		{
			//			actualProbability = ((double)itemsGoodsItem.ResultItemGoods.Count / (double)goodsItem.PurchaseHistory.Count) * 100;
			//		}
			//		System.Diagnostics.Debug.WriteLine($"\tItem.ActualProbability = {actualProbability.ToString("F2")}%");
			//	}
			//}


			return View(result);
		}
	}
}