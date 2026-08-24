using CoffeeShopDomain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopInfrastructure.Controllers
{
    public class MenuItemsController : Controller
    {
        private readonly Lab1dbContext _context;

        public MenuItemsController(Lab1dbContext context)
        {
            _context = context;
        }

        // GET: MenuItems
        public async Task<IActionResult> Index(int? categoryId)
        {
            var query = _context.Menuitems.Include(m => m.Category).AsQueryable();
            if (categoryId.HasValue)
                query = query.Where(m => m.CategoryId == categoryId);

            ViewBag.CategoryId = categoryId;
            return View(await query.ToListAsync());
        }

        // GET: MenuItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var menuitem = await _context.Menuitems.Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuitem == null) return NotFound();
            return View(menuitem);
        }

        // GET: MenuItems/Create
        public IActionResult Create(int? categoryId)
        {
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "CategoryName", categoryId);
            var menuitem = new Menuitem { CategoryId = categoryId ?? 0 };
            return View(menuitem);
        }

        // POST: MenuItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryId,ItemName,Description,ImageUrl")] Menuitem menuitem)
        {
            var category = await _context.Categories.FindAsync(menuitem.CategoryId);
            menuitem.Category = category;
            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                _context.Add(menuitem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { categoryId = menuitem.CategoryId });
            }
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "CategoryName", menuitem.CategoryId);
            return View(menuitem);
        }

        // GET: MenuItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var menuitem = await _context.Menuitems.FindAsync(id);
            if (menuitem == null) return NotFound();
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "CategoryName", menuitem.CategoryId);
            return View(menuitem);
        }

        // POST: MenuItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,ItemName,Description,ImageUrl")] Menuitem menuitem)
        {
            if (id != menuitem.Id) return NotFound();

            var category = await _context.Categories.FindAsync(menuitem.CategoryId);
            menuitem.Category = category;
            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuitem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Menuitems.Any(e => e.Id == menuitem.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index), new { categoryId = menuitem.CategoryId });
            }
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "CategoryName", menuitem.CategoryId);
            return View(menuitem);
        }

        // GET: MenuItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var menuitem = await _context.Menuitems.Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuitem == null) return NotFound();
            return View(menuitem);
        }

        // POST: MenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuitem = await _context.Menuitems.FindAsync(id);
            if (menuitem != null) _context.Menuitems.Remove(menuitem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}