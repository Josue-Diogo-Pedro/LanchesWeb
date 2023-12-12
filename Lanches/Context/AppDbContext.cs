using Lanches.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Lanches.Context;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Lanche> Lanches { get; set; }
    public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoDetalhe> PedidoDetalhes { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 

    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=172.31.6.9; Database=HeartEFCore;User ID=sa;Password=AT@123;pooling=true;TrustServerCertificate=true";
        optionsBuilder.UseSqlServer(connectionString);
    }
    */
}
