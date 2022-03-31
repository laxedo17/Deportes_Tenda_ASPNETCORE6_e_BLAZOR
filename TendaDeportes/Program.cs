using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TendaDeportes.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
//utilizase para establecer obxectos, conhecidos como services, que se pode usar en toda a app
//e accedense cunha caracteristica chamada Dependency Injection
//AddControllersWithViews establece os obxectos compartidos requeridos polas aplicacions usando o MVC Framework
//e a Razor view engine

builder.Services.AddDbContext<TendaDbContext>(opts =>
{
    opts.UseSqlServer(
        builder.Configuration["ConnectionStrings:TendaDeportesConnection"]);
});

builder.Services.AddScoped<ITendaRepository, EFTendaRepository>();
builder.Services.AddScoped<IPedidoRepository, EFPedidoRepository>();

builder.Services.AddRazorPages();
//o metodo AddScoped crea un servicio onde unha HTTP request obten o seu propio obxecto do repositorio
//que e a forma na que se usa tipicamente Entity Framework
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped<Cesta>(serv => SessionCesta.GetCesta(serv));//a lambda expression invocarase para satisfacer requests de Cesta
//a lambda expression recive unha coleccion de servicios que se rexistraron e pasa a coleccion ao metodo GetCesta da clase SessionCesta
//O resultado e que as peticions ao servicio Cesta seran manexadas creando obxectos SessionCesta, que se serializaran asi mesmos como datos de sesion cando se modifiquen
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//AddSingleton especifica que o mismo obxecto deberia ser utilizado sempre. O servicio creado di a ASP.NET Core que use a clase HttpContextAccessor
//cando as implementacions da inteface IHttpContextAccessor sexan requeridas. Este servicio e necesario de forma que podemos acceder a sesion actual na clase SessionCesta

builder.Services.AddServerSideBlazor(); //agregamos Blazor ao noso proxecto ASP.NET Core. Crea os servicios que usa Blazor

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
options.UseSqlServer(
    builder.Configuration["ConnectionStrings:IdentityConnection"]));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>();

var app = builder.Build();

if (app.Environment.IsProduction())
{
    app.UseExceptionHandler("/error");
}
app.UseRequestLocalization(opts =>
{
    opts.AddSupportedCultures("es-ES")
    .AddSupportedUICultures("es-ES")
    .SetDefaultCulture("es-ES");
});


//app.MapGet("/", () => "Hello World!");


app.UseStaticFiles(); //activa o soporte para servir contido estatico da carpeta wwwroot
app.UseSession();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute("catpage", "{categoria}/Page{productoPaxina:int}",
 new { Controller = "Home", action = "Index" }); /* / lista todas as categorias de todos os productos */

app.MapControllerRoute("page", "Page{productoPaxina:int}",
new { Controller = "Home", action = "Index", productoPaxina = 1 }); /* /Page2 Lista paxina especificada (neste casi, paxina 2), mostrando productos de todas as categorias */

app.MapControllerRoute("categoria", "{categoria}",
new { Controller = "Home", action = "Index", productoPaxina = 1 }); /* /Futbol Mostra a primeira paxina de elementos dunha categoria especifica (neste caso, categoria Futbol) */

app.MapControllerRoute("paxinacion", "Productos/Page{productoPaxina}",
new { Controller = "Home", action = "Index", productoPaxina = 1 }); /* /Futbol/Page2  Mostra paxina especificada (neste caso, paxina 2) de elementos da categoria especificada (neste caso, Futbol) */

app.MapDefaultControllerRoute();
//componente middleware especialmente importante que aporta caracteristicas de enrutado a endpoints
//o cal fai HTTP request as caracteristicas da aplicacion, conhecidas como endpoints
//capaces de producir respostas para as HTTP request
//A caracteristica endpoint routing engadese automaticamente ao request pipeline
//e MapDefaultControllerRoute rexista o MVC Framework como a orixen de endpoints
//usando unha convencion por defecto para mapear peticions a clases e metodos
app.MapRazorPages();
app.MapBlazorHub(); //ASP NET.Core soporta Blazor. Este metodo rexistra os compontes middleware de Blazor
app.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");

SemillaDatos.AsegurarsePoboarBd(app);
IdentitySeedData.EnsurePopulated(app);

app.Run();
