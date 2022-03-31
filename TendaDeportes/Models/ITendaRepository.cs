namespace TendaDeportes.Models
{
    /// <summary>
    /// Interface para usar o Repository Pattern, que aporta unha forma consistente de acceder a caracteristicas presentadas pola database context class.
    /// Un repositorio pode reducir a duplicacion e asegurase de que as operacions na base de datos se levan a cabo consistentemente.
    /// </summary>
    public interface ITendaRepository
    {
        IQueryable<Producto> Productos { get; }
        //IQueryable permite ao caller obter unha secuencia de obxectos Producto. 
        //IQueryable deriva da interface IEnumerable<T> e representa unha coleccion de obxectos aos que facerlle queries
        //tales como os usados nunha base de datos.
        //Unha clase que depende de ITendaRepository pode obter obxects Producto sen necesitar
        //saber os detalles de como se almacenan o como a implementation class os devolve.

        //operacions CRUD
        void SaveProducto(Producto p);
        void CreateProducto(Producto p);
        void DeleteProducto(Producto p);
    }
}