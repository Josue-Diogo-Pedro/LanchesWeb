using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lanches.Controllers;

public class ContactoController : Controller
{
	[Authorize]
	public IActionResult Index()
    {
        return View();
    }
}
