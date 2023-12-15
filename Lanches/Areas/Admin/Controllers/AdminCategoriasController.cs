using Lanches.Context;
using Lanches.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lanches.Areas.Admin.Controllers
{
    public class AdminCategoriasController : Controller
    {
        private readonly AppDbContext _context;

        public AdminCategoriasController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorias.ToListAsync());
        }

        //[HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var categoria = _context.Categorias
                .FirstOrDefaultAsync(cat => cat.CategoriaId == id);
            if (categoria is null) return NotFound();

            return View(categoria);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            var categoria = _context.Categorias.FirstOrDefault(cat => cat.CategoriaId == id);

            if(categoria is not null) return View(categoria);

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.CategoriaId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var categoria = _context.Categorias.FirstOrDefault(cat => cat.CategoriaId == id);

            if (categoria is null) return NotFound();

            return View(categoria);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed()

        private bool CategoriaExists(int id) =>
            _context.Categorias.Any(cat => cat.CategoriaId == id);
    }
}
