using Microsoft.AspNetCore.Mvc;
using TendaDeportes.Models;

namespace TendaDeportes.Components
{
    public class CestaResumenViewComponent : ViewComponent
    {
        private Cesta cesta;

        public CestaResumenViewComponent(Cesta cestaService)
        {
            cesta = cestaService;
        }

        public IViewComponentResult Invoke()
        {
            return View(cesta);
        }
    }
}