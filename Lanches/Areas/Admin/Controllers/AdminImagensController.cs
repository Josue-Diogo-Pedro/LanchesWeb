using Lanches.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Lanches.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize("Admin")]
public class AdminImagensController : Controller
{
    private readonly ConfigurationImagens _conf;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public AdminImagensController(IWebHostEnvironment hostingEnvironment, IOptions<ConfigurationImagens> conf)
    {
        _conf = conf.Value;
        _hostingEnvironment = hostingEnvironment;
    }

    public IActionResult Index()
    {
        return View();
    }
}
