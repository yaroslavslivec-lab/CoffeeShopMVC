using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeShopDomain.Model;

namespace CoffeeShopInfrastructure.Controllers
{
    public class ItemsizesController : Controller
    {
        private readonly Lab1dbContext _context;

        public ItemsizesController(Lab1dbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Itemsizes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var itemsize = await _context.Itemsizes.FirstOrDefaultAsync(m => m.Id == id);
            if (itemsize == null) return NotFound();
            return View(itemsize);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SizeName")] Itemsize itemsize)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemsize);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemsize);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var itemsize = await _context.Itemsizes.FindAsync(id);
            if (itemsize == null) return NotFound();
            return View(itemsize);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SizeName")] Itemsize itemsize)
        {
            if (id != itemsize.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemsize);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Itemsizes.Any(e => e.Id == itemsize.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(itemsize);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var itemsize = await _context.Itemsizes.FirstOrDefaultAsync(m => m.Id == id);
            if (itemsize == null) return NotFound();
            return View(itemsize);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemsize = await _context.Itemsizes.FindAsync(id);
            if (itemsize != null) _context.Itemsizes.Remove(itemsize);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}