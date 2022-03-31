namespace TendaDeportes.Models
{
    public class Cesta
    {
        public List<CestaLinea> Lineas { get; set; } = new List<CestaLinea>();

        public virtual void AddItem(Producto producto, int cantidade)
        {
            CestaLinea? linea = Lineas
                .Where(p => p.Producto.ProductoID == producto.ProductoID)
                .FirstOrDefault();

            if (linea == null)
            {
                Lineas.Add(new CestaLinea
                {
                    Producto = producto,
                    Cantidade = cantidade
                });
            }
            else
            {
                linea.Cantidade += cantidade;
            }
        }

        public virtual void RemoveLinea(Producto producto) => Lineas.RemoveAll(l => l.Producto.ProductoID == producto.ProductoID);

        public decimal ComputeValorTotal() => Lineas.Sum(e => e.Producto.Precio * e.Cantidade);

        public virtual void Clear() => Lineas.Clear();
    }

    public class CestaLinea
    {
        public int CestaLineaID { get; set; }
        public Producto Producto { get; set; } = new();
        public int Cantidade { get; set; }
    }
}