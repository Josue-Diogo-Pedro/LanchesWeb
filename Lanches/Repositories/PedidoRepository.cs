using Lanches.Context;
using Lanches.Models;
using Lanches.Repositories.Interfaces;

namespace Lanches.Repositories;

public class PedidoRepository : IPedidoRepostitory
{
	private readonly AppDbContext _context;
	private readonly CarrinhoCompra _carrinhoCompra;

    public PedidoRepository(AppDbContext context, 
		CarrinhoCompra carrinhoCompra)
    {
        _carrinhoCompra = carrinhoCompra;
		_context = context;
    }

    public void CriarPedido(Pedido pedido)
	{
		pedido.PedidoEnviado = DateTime.Now;
		_context.Pedidos.Add(pedido);
		_context.SaveChanges();

		var carrinhoComprtaItens = _carrinhoCompra.CarrinhoCompraItens;

		foreach (var carrinhoItem in carrinhoComprtaItens)
		{
			var pedidoDetail = new PedidoDetalhe
			{
				Quantidade = carrinhoItem.Quantidade,
				LancheId = carrinhoItem.Lanche.LancheId,
				PedidoId = pedido.PedidoId,
				Preco = carrinhoItem.Lanche.Preco
			};

			_context.PedidoDetalhes.Add(pedidoDetail);
		}
		_context.SaveChanges();
	}
}
