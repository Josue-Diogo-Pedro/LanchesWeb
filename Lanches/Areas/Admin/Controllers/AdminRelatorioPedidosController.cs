using Lanches.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Schema;

namespace Lanches.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminRelatorioPedidosController : Controller
{
    private readonly RelatorioPedidosServices _relatorioVendasServices;

    public AdminRelatorioPedidosController(RelatorioPedidosServices relatorioVendasServices)
    {
        _relatorioVendasServices = relatorioVendasServices;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> RelatorioPedidosSimples(DateTime? minDate, DateTime? maxDate)
    {
        if (!minDate.HasValue)
        {
            minDate = new DateTime(DateTime.Now.Year, 1, 1);
        }
        if (!maxDate.HasValue)
        {
            maxDate = DateTime.Now;
        }

        ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
        ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

        var result = await _relatorioVendasServices.FindByDateAsync(minDate, maxDate);

        return View(result);
    }
}
