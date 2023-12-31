﻿using Lanches.Context;
using Microsoft.EntityFrameworkCore;

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

    public int RemoverDoCarrinho(Lanche lanche)
    {
        var carrinoCompraItem = _context.CarrinhoCompraItens
            .FirstOrDefault(c => c.CarrinhoCompraId == CarrinhoCompraId &&
                c.Lanche.LancheId == lanche.LancheId);

        var quantidadeLocal = 0;

        if(carrinoCompraItem is not null)
        {
            if(carrinoCompraItem.Quantidade > 1)
            {
                carrinoCompraItem.Quantidade--;
                quantidadeLocal = carrinoCompraItem.Quantidade;
            }
            else
            {
                _context.CarrinhoCompraItens.Remove(carrinoCompraItem);
            }
        }
        _context.SaveChanges();
        return quantidadeLocal;
    }

    public List<CarrinhoCompraItem> GetCarrinhoCompraItens() => CarrinhoCompraItens ??
        (CarrinhoCompraItens =
        _context.CarrinhoCompraItens
        .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
        .Include(l => l.Lanche)
        .ToList());
    
    public void LimparCarrinho()
    {
        var carrinhoItens = _context.CarrinhoCompraItens
            .Where(c => c.CarrinhoCompraId == CarrinhoCompraId);

        _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
        _context.SaveChanges();
    }

    public decimal GetCarrinhoCompraTotal()
    {
        var total = _context.CarrinhoCompraItens
            .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
            .Select(c => c.Quantidade*c.Lanche.Preco).Sum();

        return total;
    }
}
