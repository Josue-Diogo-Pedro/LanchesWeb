using FastReport.Data;
using FastReport.Export.PdfSimple;
using FastReport.Web;
using Lanches.Areas.Admin.Services;
using Lanches.Areas.Admin.Views.FastReportUtils;
using Microsoft.AspNetCore.Mvc;

namespace Lanches.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminLanchesReportController : Controller
{
    private readonly IWebHostEnvironment _webHsotEnv;
    private readonly RelatorioLanchesService _relatorioLanchesService;

    public AdminLanchesReportController(IWebHostEnvironment webHsotEnv, RelatorioLanchesService relatorioLanchesService)
    {
        _webHsotEnv = webHsotEnv;
        _relatorioLanchesService = relatorioLanchesService;
    }

    [Route("LanchesCategoriaReport")]
    public async Task<IActionResult> LanchesCategoriaReport()
    {
        var webReport = new WebReport();
        var mssqlDataConnection = new MsSqlDataConnection();

        webReport.Report.Dictionary.AddChild(mssqlDataConnection);

        webReport.Report.Load(Path.Combine(_webHsotEnv.ContentRootPath, "wwwroot/reports", "LanchesCategoria.frx"));

        var lanches = HelperFastReport.GetDataTable(await _relatorioLanchesService.GetLanchesReportAsync(), "LanchesReport");
        var categorias = HelperFastReport.GetDataTable(await _relatorioLanchesService.GetCategoriasAsync(), "CategoriasReport");

        webReport.Report.RegisterData(lanches, "LanchesReport");
        webReport.Report.RegisterData(categorias, "CategoriasReport");

        return View(webReport);
    }

    [Route("LanchesCategoriaPDF")]
    public async Task<IActionResult> LanchesCategoriaPDF()
    {
        var webReport = new WebReport();
        var mssqlDataConnection = new MsSqlDataConnection();

        webReport.Report.Dictionary.AddChild(mssqlDataConnection);

        webReport.Report.Load(Path.Combine(_webHsotEnv.ContentRootPath, "wwwroot/reports", "LanchesCategoria.frx"));

        var lanches = HelperFastReport.GetDataTable(await _relatorioLanchesService.GetLanchesReportAsync(), "LanchesReport");
        var categorias = HelperFastReport.GetDataTable(await _relatorioLanchesService.GetCategoriasAsync(), "CategoriasReport");

        webReport.Report.RegisterData(lanches, "LanchesReport");
        webReport.Report.RegisterData(categorias, "CategoriasReport");

        webReport.Report.Prepare();

        Stream stream = new MemoryStream();

        webReport.Report.Export(new PDFSimpleExport(), stream);
        stream.Position = 0;

        return File(stream, "application/zip", "LancheCategoria.pdf");
    }
}
