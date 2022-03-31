using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TendaDeportes.Models
{
    public class Producto
    {
        public long? ProductoID { get; set; }
        [Required(ErrorMessage = "Por favor indica un nome de producto")]
        public string Nome { get; set; } = String.Empty;
        [Required(ErrorMessage = "Por favor indica unha descripcion")]
        public string Descripcion { get; set; } = String.Empty;
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Por favor entra un precio positivo")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Precio { get; set; }
        [Required(ErrorMessage = "Por favor especifica unha categoria")]
        public string Categoria { get; set; } = String.Empty;
    }
}