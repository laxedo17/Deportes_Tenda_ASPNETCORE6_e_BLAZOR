using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TendaDeportes.Models
{
    /// <summary>
    /// Parte dun arquivo database context file que actuara como unha ponte entre a base de datos e obxectos model Identity aos que proporciona acceso.
    /// A clase dervia de IdentityDbContext, que proporciona caracteristicas especificas de identidade en EF Core.
    /// Como type parameter usamos a clase IdentityUser, que e a clase integrada para representar usuarios.
    /// </summary>
    public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }
    }
}