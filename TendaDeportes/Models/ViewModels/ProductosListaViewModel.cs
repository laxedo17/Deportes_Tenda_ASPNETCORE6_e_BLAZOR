namespace TendaDeportes.Models.ViewModels
{
    public class ProductosListaViewModel
    {
        public IEnumerable<Producto> Productos { get; set; } = Enumerable.Empty<Producto>();
        public PaxinacionInfo PaxinacionInfo { get; set; } = new();
        public string? CategoriaActual { get; set; }
    }
}