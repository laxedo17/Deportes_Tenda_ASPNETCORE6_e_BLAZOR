namespace TendaDeportes.Models.ViewModels
{
    public class PaxinacionInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPorPaxina { get; set; }
        public int PaxinaActual { get; set; }

        public int TotalPaxinas =>
            (int)Math.Ceiling((decimal)TotalItems / ItemsPorPaxina);
    }
}