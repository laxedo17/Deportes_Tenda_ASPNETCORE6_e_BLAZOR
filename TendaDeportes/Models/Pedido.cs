using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TendaDeportes.Models
{
    public class Pedido
    {
        [BindNever]
        public int PedidoID { get; set; }
        [BindNever]
        public ICollection<CestaLinea> Lineas { get; set; } = new List<CestaLinea>();
        [Required(ErrorMessage = "Por favor, indica un nome")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "Por favor indica a primeira direccion")]
        public string? Linea1 { get; set; }
        public string? Linea2 { get; set; }
        public string? Linea3 { get; set; }
        [Required(ErrorMessage = "Por favor indica un lugar")]
        public string? Lugar { get; set; }
        [Required(ErrorMessage = "Por favor escribe o nome da provincia ou estado, etc")]
        public string? Provincia { get; set; }
        public string? CP { get; set; }
        [Required(ErrorMessage = "Por favor indica o pais")]
        public string? Pais { get; set; }
        public bool EnvoltorioRegalo { get; set; }

        [BindNever]
        public bool Despachado { get; set; }
    }
}