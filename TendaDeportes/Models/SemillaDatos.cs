using Microsoft.EntityFrameworkCore;

namespace TendaDeportes.Models
{
    public static class SemillaDatos
    {
        public static void AsegurarsePoboarBd(IApplicationBuilder app)
        {
            TendaDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<TendaDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Productos.Any())
            {
                context.Productos.AddRange(
                    new Producto
                    {
                        Nome = "Kayak",
                        Descripcion = "Un bote para unha persona",
                        Categoria = "Deportes acuaticos",
                        Precio = 275
                    },
                    new Producto
                    {
                        Nome = "Chaleco salvavidas",
                        Descripcion = "Protector e de moda",
                        Categoria = "Deportes acuaticos",
                        Precio = 48.95m
                    },
                    new Producto
                    {
                        Nome = "Pelota de futbol",
                        Descripcion = "Tamanho e peso aprobado pola FIFA",
                        Categoria = "Futbol",
                        Precio = 19.50m
                    },
                    new Producto
                    {
                        Nome = "Bandeiras corner",
                        Descripcion = "Dalle un toque profesional ao teu campo de xogo",
                        Categoria = "Futbol",
                        Precio = 34.95m
                    },
                    new Producto
                    {
                        Nome = "Estadio",
                        Descripcion = "Estadio 35000 asentos, moble para ensamablar na casa",
                        Categoria = "Futbol",
                        Precio = 79500
                    },
                    new Producto
                    {
                        Nome = "Boina queima neuronas",
                        Descripcion = "Millora a eficiencia do cerebro un 75%",
                        Categoria = "Xadrez",
                        Precio = 16
                    },
                    new Producto
                    {
                        Nome = "Silla inestable",
                        Descripcion = "Secretamente causalle unha desventaxa ao teu oponente",
                        Categoria = "Xadrez",
                        Precio = 29.95m
                    },
                    new Producto
                    {
                        Nome = "Tableiro de xadrez humano",
                        Descripcion = "Xogo divertido para toda a familia",
                        Categoria = "Xadrez",
                        Precio = 75
                    },
                    new Producto
                    {
                        Nome = "Bling-Bling Rey",
                        Descripcion = "Forneado en ouro, un rei forrado de diamantes",
                        Categoria = "Xadrez",
                        Precio = 1200
                    }
                );

                context.SaveChanges();
            }
        }
    }
}