using Microsoft.EntityFrameworkCore;

namespace TendaDeportes.Models
{
    /// <summary>
    /// Provee acceso a base de datos a traves dunha clase context.
    /// </summary>
    public class TendaDbContext : DbContext
    {
        public TendaDbContext(DbContextOptions<TendaDbContext> options) : base(options) { }

        public DbSet<Producto> Productos => Set<Producto>(); //property Productos da acceso a obxectos Producto da DB.
        public DbSet<Pedido> Pedidos => Set<Pedido>();
    }
}