using Lanches.Models;
using Lanches.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lanches.Controllers;

public class PedidoController : Controller
{
    private readonly IPedidoRepostitory _pedidoRepository;
    private readonly CarrinhoCompra _carrinhoCompra;

    public PedidoController(IPedidoRepostitory pedidoRepository, CarrinhoCompra carrinhoCompra)
    {
        _pedidoRepository = pedidoRepository;
        _carrinhoCompra = carrinhoCompra;
    }

    [HttpGet]
    public IActionResult Checkout()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Checkout(Pedido pedido)
    {
        return View();
    }
}
