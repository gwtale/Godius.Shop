﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godius.Shop.Data;
using Godius.Shop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Godius.Shop.Controllers
{
    public class ShopController : Controller
    {
		private readonly ApplicationDbContext _context;
		private readonly IHostingEnvironment _hostingEnvironment;
		private readonly AppOptions _appOptions;

		public ShopController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment, IOptions<AppOptions> appOptionsAccessor)
		{
			_context = context;
			_hostingEnvironment = hostingEnvironment;
			_appOptions = appOptionsAccessor.Value;
		}

		public async Task<IActionResult> Index()
        {
			var goods = await _context.Goods.ToListAsync();
            return View(goods);
        }
    }
}