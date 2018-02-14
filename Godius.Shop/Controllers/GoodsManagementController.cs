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
		public async Task<IActionResult> EditCategory(Guid? id)
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
        public async Task<IActionResult> EditCategory(Guid id, [Bind("Id,SerialCode,Name")] GoodsCategory goodsCategory)
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
		public async Task<IActionResult> DeleteCategory(Guid? id)
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
		[HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategoryConfirmed(Guid id)
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

		// GET: GoodsManagement/CreateGoods
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

		// POST: GoodsManagement/CreateGoods
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

				var goods = new Goods
				{
					Id = Guid.NewGuid(),
					SerialCode = model.SerialCode,
					Name = model.Name,
					Price = model.Price,
					Description = model.Description?.Replace(Environment.NewLine, "<br/>"),
					Category = goodsCategory
				};

				if (model.Image?.Length > 0)
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

		// GET: GoodsManagement/DetailsGoods/5
		public async Task<IActionResult> DetailsGoods(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var goods = await _context.Goods
				.SingleOrDefaultAsync(m => m.Id == id);
			if (goods == null)
			{
				return NotFound();
			}

			return View(goods);
		}

		// GET: GoodsManagement/EditGoods/5
		public async Task<IActionResult> EditGoods(Guid? id)
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

			var categorySelectListItems = from category in _context.GoodsCategories
										  orderby category.SerialCode
										  let selected = category.Id == goods.CategoryId
										  select new SelectListItem
										  {
											  Text = $"{category.Name}({category.SerialCode})",
											  Value = category.Id.ToString(),
											  Selected = selected
										  };

			ViewBag.Categories = await categorySelectListItems.ToListAsync();

			var viewModel = new EditGoodsViewModel
			{
				Id = goods.Id,
				SerialCode = goods.SerialCode,
				Name = goods.Name,
				Price = goods.Price,
				Description = goods.Description?.Replace("<br/>", Environment.NewLine),
				ImagePath = goods.Image
			};
			
			foreach (var itemGoods in goods.ItemsGoods)
			{
				await _context.Entry(itemGoods).Reference(IG => IG.Item).LoadAsync();
			}
			viewModel.ItemGoodsList.AddRange(goods.ItemsGoods.OrderByDescending(IG => IG.Item.Category).ThenBy(IG => IG.Probability));

			return View(viewModel);
		}

		// POST: GoodsManagement/EditGoods/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditGoods(Guid id, [FromForm] EditGoodsViewModel model)
		{
			if (id != model.Id)
			{
				return NotFound();
			}

			var goods = await _context.Goods.SingleOrDefaultAsync(m => m.Id == id);
			if (goods == null)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					if (model.Image?.Length > 0)
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

						// Update goods image
						goods.Image = "/" + Path.Combine(_appOptions.GoodsImagePath, fileName);
						model.ImagePath = goods.Image;
					}

					// Update goods info
					goods.SerialCode = model.SerialCode;
					goods.Name = model.Name;
					goods.Price = model.Price;
					goods.Description = model.Description?.Replace(Environment.NewLine, "<br/>");
					goods.CategoryId = Guid.Parse(model.CategoryId);

					_context.Update(goods);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!GoodsExists(model.Id))
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
			return View(model);
		}

		// GET: GoodsManagement/DeleteGoods/5
		public async Task<IActionResult> DeleteGoods(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var goods = await _context.Goods
				.SingleOrDefaultAsync(m => m.Id == id);
			if (goods == null)
			{
				return NotFound();
			}

			return View(goods);
		}

		// POST: GoodsManagement/DeleteGoods/5
		[HttpPost, ActionName("DeleteGoods")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteGoodsConfirmed(Guid id)
		{
			var goods = await _context.Goods.SingleOrDefaultAsync(m => m.Id == id);
			_context.Goods.Remove(goods);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool GoodsExists(Guid id)
		{
			return _context.Goods.Any(e => e.Id == id);
		}

		#endregion

		#region ItemGoods

		// GET: GoodsManagement/CreateItemGoods
		public async Task<IActionResult> CreateItemGoods(Guid id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var goods = await _context.Goods
				.SingleOrDefaultAsync(m => m.Id == id);
			if (goods == null)
			{
				return NotFound();
			}
			
			ViewData["ItemsName"] = new SelectList(_context.Items, "Id", "Name");
			return View(new ItemGoods { GoodsId = goods.Id });
		}

		// POST: GoodsManagement/CreateItemGoods
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateItemGoods([Bind("Id,Probability,Description,GoodsId,ItemId")] ItemGoods itemGoods)
		{
			if (ModelState.IsValid)
			{
				itemGoods.Id = Guid.NewGuid();
				_context.Add(itemGoods);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(EditGoods), new { id = itemGoods.GoodsId });
			}
			
			ViewData["ItemsName"] = new SelectList(_context.Items, "Id", "Name", itemGoods.ItemId);
			return View(itemGoods);
		}

		// GET: GoodsManagement/DetailsItemGoods/5
		public async Task<IActionResult> DetailsItemGoods(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var itemGoods = await _context.ItemGoods
				.Include(i => i.Goods)
				.Include(i => i.Item)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (itemGoods == null)
			{
				return NotFound();
			}

			return View(itemGoods);
		}

		// GET: GoodsManagement/EditItemGoods/5
		public async Task<IActionResult> EditItemGoods(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var itemGoods = await _context.ItemGoods.Include(IG => IG.Item).SingleOrDefaultAsync(m => m.Id == id);
			if (itemGoods == null)
			{
				return NotFound();
			}

			ViewData["ItemsName"] = new SelectList(_context.Items, "Id", "Name", itemGoods.ItemId);
			return View(itemGoods);
		}

		// POST: GoodsManagement/EditItemGoods/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditItemGoods(Guid id, [Bind("Id,Probability,Description,GoodsId,ItemId")] ItemGoods itemGoods)
		{
			if (id != itemGoods.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(itemGoods);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ItemGoodsExists(itemGoods.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(EditGoods), new { id = itemGoods.GoodsId });
			}

			ViewData["ItemsName"] = new SelectList(_context.Items, "Id", "Name", itemGoods.ItemId);
			return View(itemGoods);
		}

		// GET: GoodsManagement/DeleteItemGoods/5
		public async Task<IActionResult> DeleteItemGoods(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var itemGoods = await _context.ItemGoods
				.Include(i => i.Goods)
				.Include(i => i.Item)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (itemGoods == null)
			{
				return NotFound();
			}

			return View(itemGoods);
		}

		// POST: GoodsManagement/DeleteItemGoods/5
		[HttpPost, ActionName("DeleteItemGoods")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var itemGoods = await _context.ItemGoods.SingleOrDefaultAsync(m => m.Id == id);
			_context.ItemGoods.Remove(itemGoods);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(EditGoods), new { id = itemGoods.GoodsId });
		}

		private bool ItemGoodsExists(Guid id)
		{
			return _context.ItemGoods.Any(e => e.Id == id);
		}

		#endregion
	}
}
