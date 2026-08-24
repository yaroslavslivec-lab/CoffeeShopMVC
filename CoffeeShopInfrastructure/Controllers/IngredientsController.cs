using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeShopDomain.Model;

namespace CoffeeShopInfrastructure.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly Lab1dbContext _context;

        public IngredientsController(Lab1dbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Ingredients.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(m => m.Id == id);
            if (ingredient == null) return NotFound();
            return View(ingredient);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IngredientName")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null) return NotFound();
            return View(ingredient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IngredientName")] Ingredient ingredient)
        {
            if (id != ingredient.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Ingredients.Any(e => e.Id == ingredient.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(m => m.Id == id);
            if (ingredient == null) return NotFound();
            return View(ingredient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient != null) _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}