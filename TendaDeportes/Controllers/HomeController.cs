using Microsoft.AspNetCore.Mvc;
using TendaDeportes.Models;
using TendaDeportes.Models.ViewModels;

namespace TendaDeportes.Controllers
{
    public class HomeController : Controller
    {
        private ITendaRepository repositorio;
        public int TamanhoPaxina = 4; //especifica que queremos 4 productos por paxina

        /// <summary>
        /// Cando ASP.NET Core cree unha nova instancia da clase HomeController para manexar un HTTP request,
        /// inspeccionara o constructor e vera que require un obxecto que implementa a interface IRepository.
        /// Para determinar que clase debe usar, ASP.NET Core consultara a configuracion creada en Program.cs
        /// que indica que EFTendaRepository deberia usarse e que unha nova instancia deberia crear para cada request.
        /// ASP.NET Core crear un obxecto EFTendaRepository e usao para chamar ao constructor de HomeController
        /// para crear un obxecto controller que procesara un HTTP request. 
        /// Isto chamase Dependency Injection.
        /// Este medoto permite a un obxecto HomeController acceder ao repositorio da aplicacion a traves da interfaz ITendaRepository
        /// sen conhecer que implementation class foi configurada.
        /// Poderiamos reconfigurar o servicio para usar unha implementation class diferente -unha que non use EF por exemplo-
        /// e Dependency Injection significa que o controller seguira funcionando sen cambios.
        /// </summary>
        /// <param name="repo"></param>
        public HomeController(ITendaRepository repo)
        {
            repositorio = repo;
        }

        /// <summary>
        /// Engadimos un optional parameter a Index, o cal significa que si o metodo se chama sin un parametro
        /// a chamada tratase como si aportaramos o valor especificado na definicion do parametro,
        /// co efecto que o actionh method mostra a primeira paxina de productos cando se invoca sen argumento.
        /// Dentro do body do action method, facemos get a obxectos Producto, ordenamolos pola primary key,
        /// evitamos (skip over) os productos que tenhen seu lugar antes do comenzo da paxina actual
        /// e tomamos o numero de productos especificados polo campo TamanhoPaxina.
        /// </summary>
        /// <param name="productoPaxina"></param>
        /// <returns></returns>
        public ViewResult Index(string? categoria, int productoPaxina = 1)
        => View(new ProductosListaViewModel
        {
            Productos = repositorio.Productos
                .Where(p => categoria == null || p.Categoria == categoria)
                .OrderBy(p => p.ProductoID)
                .Skip((productoPaxina - 1) * TamanhoPaxina)
                .Take(TamanhoPaxina),
            PaxinacionInfo = new PaxinacionInfo
            {
                PaxinaActual = productoPaxina,
                ItemsPorPaxina = TamanhoPaxina,
                TotalItems = categoria == null ? repositorio.Productos.Count() : repositorio.Productos.Where(e =>
                    e.Categoria == categoria).Count()
            },
            CategoriaActual = categoria
        });
    }
}