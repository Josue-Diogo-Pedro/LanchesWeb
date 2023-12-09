using Lanches.Models;
using Lanches.Repositories.Interfaces;
using Lanches.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lanches.Controllers;

public class CarrinhoCompraController : Controller
{
	private readonly ILancheRepository _lancheRepository;
	private readonly CarrinhoCompra _carrinhoCompra;

    public CarrinhoCompraController(ILancheRepository lancheRepository,
									CarrinhoCompra carrinhoCompra)
    {
        _lancheRepository = lancheRepository;
		_carrinhoCompra = carrinhoCompra;
    }

    public IActionResult Index()
	{
		var itens = _carrinhoCompra.GetCarrinhoCompraItens();
		_carrinhoCompra.CarrinhoCompraItens = itens;

		var carrinhoCompraVM = new CarrinhoCompraViewModel
		{
			CarrinhoCompra = _carrinhoCompra,
			CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
		};

		return View(carrinhoCompraVM);
	}

	public IActionResult AdidiconarItemNoCarrinho(int lancheId)
	{
		var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);

		if(lancheSelecionado is not null)
		{
			_carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);
		}

		return RedirectToAction(nameof(Index));
	}

	public IActionResult RemoverDoCarrinho(int lancheId)
	{
		var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);

		if(lancheSelecionado is not null)
		{
			_carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
		}

		return RedirectToAction(nameof(Index));
	}
}
