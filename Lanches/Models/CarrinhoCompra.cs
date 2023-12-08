using Lanches.Context;

namespace Lanches.Models;

public class CarrinhoCompra
{
    public string CarrinhoCompraId { get; set; }
    public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

	private readonly AppDbContext _context;

    public CarrinhoCompra(AppDbContext context)
    {
        _context = context;
    }

    public static CarrinhoCompra GetCarrinho(IServiceProvider service)
    {

        //Define uma Sessão
        ISession session =
            service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

        //Obtem um serviço do tipo do nosso Contexto
        var context = service.GetService<AppDbContext>();

        //Obtem ou gera Id do carrinho
        string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

        //Atribui o Id do carrinho na sessão
        session.SetString("CarrinhoId", carrinhoId);

        //Retorna o carrinho com o contexto e o Id Atribuido ou obtido
        return new CarrinhoCompra(context)
        {
            CarrinhoCompraId = carrinhoId
        };
    }

    public void AdicionarAoCarrinho(Lanche lanche)
    {
        var carrinhoCompraItem = _context.CarrinhoCompraItens
            .FirstOrDefault(c => c.CarrinhoCompraId == CarrinhoCompraId
            && c.Lanche.LancheId == lanche.LancheId);

        if(carrinhoCompraItem is null)
        {
            carrinhoCompraItem = new CarrinhoCompraItem
            {
                CarrinhoCompraId = CarrinhoCompraId,
                Lanche = lanche,
                Quantidade = 1
            };
            _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
        }
        else
        {
            carrinhoCompraItem.Quantidade++;
        }
        _context.SaveChanges();
    }
}
