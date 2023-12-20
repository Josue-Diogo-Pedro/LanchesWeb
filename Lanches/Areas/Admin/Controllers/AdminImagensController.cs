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

    public async Task<IActionResult> UploadFiles(List<IFormFile> files)
    {
        if(files is null || files.Count == 0)
        {
            ViewData["Error"] = "Error: Arquivo(s) não selecionado(s)!";
            return View(ViewData);
        }
        if(files.Count > 10)
        {
            ViewData["Error"] = "Error: Quantidade de arquivos excedeu ao limite!";
            return View(ViewData);
        }

        long size = files.Sum(f => f.Length);

        var filePathsName = new List<string>();

        var filePath = Path.Combine(_hostingEnvironment.WebRootPath, _conf.NomePastaImagensProdutos);

        foreach (var formFile in files)
        {
            if(formFile.FileName.Contains(".jpg") || 
                formFile.FileName.Contains(".gif") || 
                formFile.FileName.Contains(".png"))
            {
                var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);

                filePathsName.Add(fileNameWithPath);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }
        }

        ViewData["Resultado"] = $"{files.Count} arquivos foram enviados ao servidor com tamanho total de : {size} bytes";

        ViewBag.Arquivos = filePathsName;

        return View(ViewData);
    }
}
