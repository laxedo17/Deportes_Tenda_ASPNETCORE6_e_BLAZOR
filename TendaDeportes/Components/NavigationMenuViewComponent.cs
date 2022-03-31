using Microsoft.AspNetCore.Mvc;
using TendaDeportes.Models;

namespace TendaDeportes.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private ITendaRepository repositorio;

        public NavigationMenuViewComponent(ITendaRepository repo)
        {
            repositorio = repo;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["categoria"]; //a property SelectedCategory e dynamic e asignase dinamicamente
            return View(repositorio.Productos
                .Select(x => x.Categoria)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}