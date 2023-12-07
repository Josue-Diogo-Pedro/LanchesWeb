using Lanches.Repositories.Interfaces;
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
        ViewData["Titulo"] = "Todos os Lanches";

        var lanches = _lancheRepository.Lanches;

        return View(lanches);
    }
}
