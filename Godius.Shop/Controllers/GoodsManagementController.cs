using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Godius.Shop.Data;
using Godius.Shop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System.IO;
using Godius.Shop.Models.GoodsViewModels;

namespace Godius.Shop.Controllers
{
    public class GoodsManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
		private readonly IHostingEnvironment _hostingEnvironment;
		private readonly AppOptions _appOptions;

		public GoodsManagementController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment, IOptions<AppOptions> appOptionsAccessor)
		{
			_context = context;
			_hostingEnvironment = hostingEnvironment;
			_appOptions = appOptionsAccessor.Value;
		}

        // GET: GoodsManagement
        public async Task<IActionResult> Index()
        {
			ViewBag.Categories = await _context.GoodsCategories.OrderBy(C => C.SerialCode).ToListAsync();
			ViewBag.Goods = await _context.Goods.OrderBy(G => G.SerialCode).ToListAsync();

			return View();
        }

		#region Category

		// GET: GoodsManagement/DetailsCategory/5
		public async Task<IActionResult> DetailsCategory(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goodsCategory = await _context.GoodsCategories
				.SingleOrDefaultAsync(m => m.Id == id);
            if (goodsCategory == null)
            {
                return NotFound();
            }

            return View(goodsCategory);
        }

        // GET: GoodsManagement/CreateCategory
        public IActionResult CreateCategory()
        {
            return View();
        }

		// POST: GoodsManagement/CreateCreateCategory
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory([Bind("Id,SerialCode,Name")] GoodsCategory goodsCategory)
        {
            if (ModelState.IsValid)
            {
                goodsCategory.Id = Guid.NewGuid();
                _context.Add(goodsCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(goodsCategory);
        }

		// GET: GoodsManagement/EditCreateCategory/5
		public async Task<IActionResult> EditCreateCategory(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goodsCategory = await _context.GoodsCategories.SingleOrDefaultAsync(m => m.Id == id);
            if (goodsCategory == null)
            {
                return NotFound();
            }
            return View(goodsCategory);
        }

		// POST: GoodsManagement/EditCreateCategory/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCreateCategory(Guid id, [Bind("Id,SerialCode,Name")] GoodsCategory goodsCategory)
        {
            if (id != goodsCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(goodsCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoodsCategoryExists(goodsCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(goodsCategory);
        }

		// GET: GoodsManagement/DeleteCreateCategory/5
		public async Task<IActionResult> DeleteCreateCategory(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goodsCategory = await _context.GoodsCategories
				.SingleOrDefaultAsync(m => m.Id == id);
            if (goodsCategory == null)
            {
                return NotFound();
            }

            return View(goodsCategory);
        }

		// POST: GoodsManagement/DeleteCreateCategory/5
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var goodsCategory = await _context.GoodsCategories.SingleOrDefaultAsync(m => m.Id == id);
            _context.GoodsCategories.Remove(goodsCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GoodsCategoryExists(Guid id)
        {
            return _context.GoodsCategories.Any(e => e.Id == id);
        }

		#endregion

		#region Goods

		// GET: Goods/CreateGoods
		public async Task<IActionResult> CreateGoods()
		{
			ViewBag.Categories = await _context.GoodsCategories
													.OrderBy(C => C.SerialCode)
													.Select(C => new SelectListItem
													{
														Text = $"{C.Name}({C.SerialCode})",
														Value = C.Id.ToString()
													})
													.ToListAsync();
			return View();
		}

		// POST: Goods/CreateGoods
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateGoods([FromForm] CreateGoodsViewModel model)
		{
			if (ModelState.IsValid)
			{
				var goodsCategory = await _context.GoodsCategories
				.SingleOrDefaultAsync(m => m.Id == Guid.Parse(model.CategoryId));
				if (goodsCategory == null)
				{
					return NotFound();
				}

				var goods = new Goods { Id = Guid.NewGuid(), SerialCode = model.SerialCode, Name = model.Name, Price = model.Price, Category = goodsCategory };

				if (model.Image.Length > 0)
				{
					var dirPath = Path.Combine(_hostingEnvironment.WebRootPath, _appOptions.GoodsImagePath);
					var dirInfo = new DirectoryInfo(dirPath);
					if (dirInfo.Exists != true)
					{
						dirInfo.Create();
					}

					var fileName = Path.GetFileName(model.Image.FileName);
					var fileFullPath = Path.Combine(dirInfo.FullName, fileName);
					using (var stream = new FileStream(fileFullPath, FileMode.Create))
					{
						await model.Image.CopyToAsync(stream);
					}

					goods.Image = "/" + Path.Combine(_appOptions.GoodsImagePath, fileName);
				}

				_context.Add(goods);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}

		#endregion
	}
}
