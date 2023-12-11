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
        int totalItensPedido = 0;
        decimal precoTotalPedido = 0.0m;

        //Obtem os itens do carrinho de compra do cliente
        List<CarrinhoCompraItem> itens = _carrinhoCompra.GetCarrinhoCompraItens();
        _carrinhoCompra.CarrinhoCompraItens = itens;

        //Verifica se existem itens de pedido
        if(_carrinhoCompra.CarrinhoCompraItens.Count == 0)
        {
            ModelState.AddModelError("", "O seu carrinho está vazio, que tal incluir um lanche?");
        }

        foreach (var item in itens)
        {
            totalItensPedido += item.Quantidade;
            precoTotalPedido += (item.Lanche.Preco * item.Quantidade);
        }

        //Atribui os valores ao pedido
        pedido.TotalItensPedido = totalItensPedido;
        pedido.PedidoTotal = precoTotalPedido;

        //Valida os dados do pedido
        if (ModelState.IsValid)
        {
            //Cria o pedido e os detalhes
            _pedidoRepository.CriarPedido(pedido);

            //Define mensagens do cliente
            ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
            ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

            //Limpa o carrinho do cliente
            _carrinhoCompra.LimparCarrinho();

            //Exibe a view com dos dados do pedido e do cliente
            return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
        }

        return View(pedido);
    }
}
