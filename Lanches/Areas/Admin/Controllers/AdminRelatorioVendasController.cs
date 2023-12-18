using Microsoft.AspNetCore.Mvc;

namespace Lanches.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRelatorioVendasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
