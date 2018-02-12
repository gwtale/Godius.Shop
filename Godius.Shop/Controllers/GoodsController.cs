using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Godius.Shop.Data;
using Godius.Shop.Models;
using Godius.Shop.Models.GoodsViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Godius.Shop.Controllers
{
    public class GoodsController : Controller
    {
        private readonly ApplicationDbContext _context;
		private readonly IHostingEnvironment _hostingEnvironment;
		private readonly AppOptions _appOptions;

		public GoodsController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment, IOptions<AppOptions> appOptionsAccessor)
        {
            _context = context;
			_hostingEnvironment = hostingEnvironment;
			_appOptions = appOptionsAccessor.Value;
		}

        // GET: Goods
        public async Task<IActionResult> Index()
        {
            return View(await _context.Goods.ToListAsync());
        }

        // GET: Goods/Details/5
        public async Task<IActionResult> Details(Guid? id)
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

        // GET: Goods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Goods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([FromForm] CreateGoodsViewModel model)
		{
			if (ModelState.IsValid)
			{
				var goods = new Goods { Id = Guid.NewGuid(), SerialCode = model.SerialCode, Name = model.Name, Price = model.Price };

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

		// GET: Goods/Edit/5
		public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goods = await _context.Goods.SingleOrDefaultAsync(m => m.Id == id);
            if (goods == null)
            {
                return NotFound();
            }
            return View(new EditGoodsViewModel { Id = goods.Id, SerialCode = goods.SerialCode, Name = goods.Name, Price = goods.Price, ImagePath = goods.Image });
        }

        // POST: Goods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [FromForm] EditGoodsViewModel model)
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

        // GET: Goods/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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

        // POST: Goods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
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
    }
}
