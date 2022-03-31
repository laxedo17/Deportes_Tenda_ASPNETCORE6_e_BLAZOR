namespace TendaDeportes.Models
{
    public class EFTendaRepository : ITendaRepository
    {
        private TendaDbContext context;

        public EFTendaRepository(TendaDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Producto> Productos => context.Productos;
        //mapeamos a property Productos definida na interface ITendaRepository
        //na propiedade Productos definida na clase TendaDbContext.
        //a property Producto na clase context devolve un obxecto DbSet<Producto>

        public void CreateProducto(Producto p)
        {
            context.Add(p);
            context.SaveChanges();
        }

        public void DeleteProducto(Producto p)
        {
            context.Remove(p);
            context.SaveChanges();
        }

        public void SaveProducto(Producto p)
        {
            context.SaveChanges();
        }
    }
}