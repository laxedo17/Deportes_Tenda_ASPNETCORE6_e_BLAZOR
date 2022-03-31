using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Infrastructure;
using TendaDeportes.Infrastructura;
using TendaDeportes.Models;
namespace TendaDeportes.Pages
{
    public class CestaModel : PageModel
    {
        private ITendaRepository repository;
        public CestaModel(ITendaRepository repo, Cesta cestaService)
        {
            repository = repo;
            Cesta = cestaService;
        }
        public Cesta Cesta { get; set; }
        public string ReturnUrl { get; set; } = "/";
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            //Cesta = HttpContext.Session.GetJson<Cesta>("cesta") ?? new Cesta();
        }
        public IActionResult OnPost(long productoId, string returnUrl)
        {
            Producto? producto = repository.Productos
                .FirstOrDefault(p => p.ProductoID == productoId);
            if (producto != null)
            {
                //Cesta = HttpContext.Session.GetJson<Cesta>("cesta") ?? new Cesta();
                Cesta.AddItem(producto, 1);
                //HttpContext.Session.SetJson("cesta", Cesta);
            }
            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(long productoId, string returnUrl)
        {
            Cesta.RemoveLinea(Cesta.Lineas.First(cl =>
            cl.Producto.ProductoID == productoId).Producto);
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}