using Lanches.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lanches.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminGraficoController : Controller
{
    private readonly GraficoVendasService _graficoVendas;

    public AdminGraficoController(GraficoVendasService graficoVendas)
    {
        _graficoVendas = graficoVendas ?? throw new ArgumentNullException(nameof(GraficoVendasService));
    }

    public JsonResult VendasLanches(int dias)
    {
        var lanchesVendasTotais = _graficoVendas.GetVendasLanches(dias);
        return Json(lanchesVendasTotais);
    }


    public IActionResult Index() 
    {
        return View();
    }

    [HttpGet]
    public IActionResult VendasMensais(int dias)
    {
        return View();
    }

    [HttpGet]
    public IActionResult VendasSemanais(int dias)
    { 
        return View();
    }

}
