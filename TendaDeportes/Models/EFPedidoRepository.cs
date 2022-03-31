using Microsoft.EntityFrameworkCore;

namespace TendaDeportes.Models
{
    public class EFPedidoRepository : IPedidoRepository
    {
        private TendaDbContext context;

        public EFPedidoRepository(TendaDbContext ctx)
        {
            context = ctx;
        }

        //a coleccion asociada coa propiedade Lineas deberia cargarse con cada obxecto Producto asociaco con cada obxecto da coleccion
        public IQueryable<Pedido> Pedidos => context.Pedidos
            .Include(p => p.Lineas)
            .ThenInclude(l => l.Producto);
        //isto asegura que recibimos todos os data objects que necesitamos sen ter que realizar queries por separado e ensambla os datos nos

        public void SavePedido(Pedido pedido)
        {
            context.AttachRange(pedido.Lineas.Select(l => l.Producto));
            if (pedido.PedidoID == 0)
            {
                context.Pedidos.Add(pedido);
            }

            context.SaveChanges();
        }
    }
}