using Microsoft.AspNetCore.Mvc;

namespace Lanches.Areas.Admin.Controllers;

public class AdminGraficoController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
