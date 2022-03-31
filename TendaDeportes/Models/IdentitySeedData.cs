using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TendaDeportes.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Secret123$";

        /// <summary>
        /// Aseguramos que se crea e actualiza a base de datos e usa a clase UserManager<T>, que proporciona ASP.NET Core Identity como servicio para xestionar usuarios.
        /// Buscamos na base de datos pola conta de usuario Admin, a cal se crea, con password, se non está presente. Hardcodeamos o password con esos caracteres porque Identity ten unhas politicas de validacion que require certos requitisos como un numero e un rango de caracteres.
        /// </summary>
        /// <param name="app"></param>
        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            AppIdentityDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<AppIdentityDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            UserManager<IdentityUser> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser user = await userManager.FindByNameAsync(adminUser);
            if (user == null)
            {
                user = new IdentityUser("Admin");
                user.Email = "admin@example.com";
                user.PhoneNumber = "888-4321";
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}