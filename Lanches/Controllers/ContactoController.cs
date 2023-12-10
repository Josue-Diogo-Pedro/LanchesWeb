using Microsoft.AspNetCore.Mvc;

namespace Lanches.Controllers;

public class ContactoController : Controller
{

    public IActionResult Index()
    {
        return View();
    }
}
