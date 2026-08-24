using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoffeeShopDomain.Model;

namespace CoffeeShopInfrastructure.Controllers
{
    public class ItemvariationsController : Controller
    {
        private readonly Lab1dbContext _context;

        public ItemvariationsController(Lab1dbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var query = _context.Itemvariations
                .Include(v => v.MenuItem)
                .Include(v => v.Size);
            return View(await query.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var itemvariation = await _context.Itemvariations
                .Include(v => v.MenuItem).Include(v => v.Size)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemvariation == null) return NotFound();
            return View(itemvariation);
        }

        public IActionResult Create()
        {
            ViewBag.MenuItemId = new SelectList(_context.Menuitems, "Id", "ItemName");
            ViewBag.SizeId = new SelectList(_context.Itemsizes, "Id", "SizeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MenuItemId,SizeId,Price")] Itemvariation itemvariation)
        {
            itemvariation.MenuItem = await _context.Menuitems.FindAsync(itemvariation.MenuItemId);
            itemvariation.Size = await _context.Itemsizes.FindAsync(itemvariation.SizeId);
            ModelState.Remove("MenuItem");
            ModelState.Remove("Size");

            if (ModelState.IsValid)
            {
                _context.Add(itemvariation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MenuItemId = new SelectList(_context.Menuitems, "Id", "ItemName", itemvariation.MenuItemId);
            ViewBag.SizeId = new SelectList(_context.Itemsizes, "Id", "SizeName", itemvariation.SizeId);
            return View(itemvariation);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var itemvariation = await _context.Itemvariations.FindAsync(id);
            if (itemvariation == null) return NotFound();
            ViewBag.MenuItemId = new SelectList(_context.Menuitems, "Id", "ItemName", itemvariation.MenuItemId);
            ViewBag.SizeId = new SelectList(_context.Itemsizes, "Id", "SizeName", itemvariation.SizeId);
            return View(itemvariation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MenuItemId,SizeId,Price")] Itemvariation itemvariation)
        {
            if (id != itemvariation.Id) return NotFound();

            itemvariation.MenuItem = await _context.Menuitems.FindAsync(itemvariation.MenuItemId);
            itemvariation.Size = await _context.Itemsizes.FindAsync(itemvariation.SizeId);
            ModelState.Remove("MenuItem");
            ModelState.Remove("Size");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemvariation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Itemvariations.Any(e => e.Id == itemvariation.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MenuItemId = new SelectList(_context.Menuitems, "Id", "ItemName", itemvariation.MenuItemId);
            ViewBag.SizeId = new SelectList(_context.Itemsizes, "Id", "SizeName", itemvariation.SizeId);
            return View(itemvariation);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var itemvariation = await _context.Itemvariations
                .Include(v => v.MenuItem).Include(v => v.Size)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemvariation == null) return NotFound();
            return View(itemvariation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemvariation = await _context.Itemvariations.FindAsync(id);
            if (itemvariation != null) _context.Itemvariations.Remove(itemvariation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}