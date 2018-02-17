using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Godius.Shop.Data;
using Godius.Shop.Models;
using Godius.Shop.Models.ItemViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace Godius.Shop.Controllers
{
	[Authorize(Roles = "Admin")]
	public class ItemsManagementController : Controller
    {
		private readonly ApplicationDbContext _context;
		private readonly IHostingEnvironment _hostingEnvironment;
		private readonly AppOptions _appOptions;
		
		public ItemsManagementController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment, IOptions<AppOptions> appOptionsAccessor)
		{
			_context = context;
			_hostingEnvironment = hostingEnvironment;
			_appOptions = appOptionsAccessor.Value;
		}

		// GET: ItemsManagement
		public async Task<IActionResult> Index()
        {
            return View(await _context.Items.OrderBy(I => I.Category).ThenBy(I => I.Generation).ToListAsync());
        }

        // GET: ItemsManagement/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .SingleOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: ItemsManagement/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ItemsManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([FromForm] CreateItemViewModel model)
		{
			if (ModelState.IsValid)
			{
				var item = new Item
				{
					Id = Guid.NewGuid(),
					Category = model.Category,
					Name = model.Name,
					Description = model.Description?.Replace(Environment.NewLine, "<br/>"),
					Weight = model.Weight,
					WC = model.WC,
					AC = model.AC,
					HC = model.HC,
					DC = model.DC,
					Durability = model.Durability,
					Generation = model.Generation
				};

				if (model.Image?.Length > 0)
				{
					var dirPath = Path.Combine(_hostingEnvironment.WebRootPath, _appOptions.ItemImagePath);
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

					item.Image = "/" + Path.Combine(_appOptions.ItemImagePath, fileName);
				}

				_context.Add(item);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}

		// GET: ItemsManagement/Edit/5
		public async Task<IActionResult> Edit(Guid? id)
        {
			if (id == null)
			{
				return NotFound();
			}

			var item = await _context.Items.SingleOrDefaultAsync(m => m.Id == id);
			if (item == null)
			{
				return NotFound();
			}

			return View(new EditItemViewModel
			{
				Id = item.Id,
				Category = item.Category,
				Name = item.Name,
				Description = item.Description?.Replace("<br/>", Environment.NewLine),
				Weight = item.Weight,
				WC = item.WC,
				AC = item.AC,
				HC = item.HC,
				DC = item.DC,
				Durability = item.Durability,
				Generation = item.Generation,
				ImagePath = item.Image
			});
		}

        // POST: ItemsManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [FromForm] EditItemViewModel model)
        {
			if (id != model.Id)
			{
				return NotFound();
			}

			var item = await _context.Items.SingleOrDefaultAsync(m => m.Id == id);
			if (item == null)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					if (model.Image?.Length > 0)
					{
						var dirPath = Path.Combine(_hostingEnvironment.WebRootPath, _appOptions.ItemImagePath);
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
						item.Image = "/" + Path.Combine(_appOptions.ItemImagePath, fileName);
						model.ImagePath = item.Image;
					}

					// Update Item info
					item.Category = model.Category;
					item.Name = model.Name;					
					item.Description = model.Description?.Replace(Environment.NewLine, "<br/>");
					item.Weight = model.Weight;
					item.WC = model.WC;
					item.AC = model.AC;
					item.HC = model.HC;
					item.DC = model.DC;
					item.Durability = model.Durability;
					item.Generation = model.Generation;

					_context.Update(item);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ItemExists(model.Id))
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

        // GET: ItemsManagement/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .SingleOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: ItemsManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var item = await _context.Items.SingleOrDefaultAsync(m => m.Id == id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(Guid id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
