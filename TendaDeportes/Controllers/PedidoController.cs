using Microsoft.AspNetCore.Mvc;
using TendaDeportes.Models;

namespace TendaDeportes.Controllers
{
    public class PedidoController : Controller
    {
        private IPedidoRepository repositorio;
        private Cesta cesta;

        public PedidoController(IPedidoRepository repoService, Cesta cestaService)
        {
            repositorio = repoService;
            cesta = cestaService;
        }
        public ViewResult Pagamento() => View(new Pedido()); //devolve a View por defecto e pasa un obxecto Pedido como view model.
                                                             //para crear a view, creamos unha carpeta Views/Pedido e engadimoslle unha Razor View chamada Pagamento.cshtml

        [HttpPost]
        public IActionResult Pagamento(Pedido pedido)
        {
            if (cesta.Lineas.Count() == 0)
            {
                ModelState.AddModelError("", "Sentimolo, a tua cesta esta baleira");
            }
            if (ModelState.IsValid)
            {
                pedido.Lineas = cesta.Lineas.ToArray();
                repositorio.SavePedido(pedido);
                cesta.Clear();
                return RedirectToPage("/Completado", new { pedidoId = pedido.PedidoID });
            }
            else
            {
                return View();
            }
        }
    }
}