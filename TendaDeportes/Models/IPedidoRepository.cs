namespace TendaDeportes.Models
{
    /// <summary>
    /// Proporciona acceso aos obxectos Pedido
    /// </summary>
    public interface IPedidoRepository
    {
        IQueryable<Pedido> Pedidos { get; }
        void SavePedido(Pedido pedido);
    }
}