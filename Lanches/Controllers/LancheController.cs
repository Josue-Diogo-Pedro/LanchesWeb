using Lanches.Repositories.Interfaces;
using Lanches.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lanches.Controllers;

public class LancheController : Controller
{
    private readonly ILancheRepository _lancheRepository;

    public LancheController(ILancheRepository lancheRepository)
    {
        _lancheRepository = lancheRepository;
    }

    public IActionResult List()
    {
        //var lanches = _lancheRepository.Lanches;
        //return View(lanches);
        var lancheListViewModel = new LancheListViewModel();
        lancheListViewModel.Lanches = _lancheRepository.Lanches;
        lancheListViewModel.CategoriaAtual = "Categoria Atual";

        return View(lancheListViewModel);
    }
}
