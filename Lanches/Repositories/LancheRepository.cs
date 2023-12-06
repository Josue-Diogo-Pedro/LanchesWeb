using Lanches.Context;
using Lanches.Models;
using Lanches.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lanches.Repositories;

public class LancheRepository : ILancheRepository
{
    private readonly AppDbContext _context;

    public LancheRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Lanche> Lanches => _context.Lanches.Include(l => l.Categoria);

    public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches
        .Where(l => l.IsLanchePreferido)
        .Include(l => l.Categoria);

    public Lanche GetLancheById(int id) =>
        _context.Lanches.FirstOrDefault(l => l.LancheId == id);

}
