using Lanches.Context;
using Lanches.Models;

namespace Lanches.Areas.Admin.Services;

public class GraficoVendasService
{
    public readonly AppDbContext _context;

    public GraficoVendasService(AppDbContext context)
    {
        _context = context;
    }

    public List<LancheGrafico> GetVendasLanches(int dias = 360)
    {
        var data = DateTime.Now.AddDays(-dias);

        var lanches = from pd in _context.PedidoDetalhes
                       join l in _context.Lanches on pd.LancheId equals l.LancheId
                       where pd.Pedido.PedidoEnviado >= data
                       group pd by new { pd.PedidoId, l.Nome, pd.Quantidade }
                       into g
                       select new
                       {
                           LancheNome = g.Key.Nome,
                           LanchesQuantidade = g.Sum(q => q.Quantidade),
                           LanchesValorTotal = g.Sum(vt => vt.Quantidade * vt.Lanche.Preco)
                       };

        List<LancheGrafico> graficosLanche = new();

        foreach (var item in lanches)
        {
            var lanche = new LancheGrafico();
            lanche.LancheNome = item.LancheNome;
            lanche.LancheQuantidade = item.LanchesQuantidade;
            lanche.LancheValorTotal = item.LanchesValorTotal;

            graficosLanche.Add(lanche);
        }

        return graficosLanche;
    }
}
