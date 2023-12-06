using Lanches.Context;
using Lanches.Models;
using Lanches.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lanches.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;
    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Categoria> Categorias => _context.Categorias.Include(c => c.Lanches);
}
