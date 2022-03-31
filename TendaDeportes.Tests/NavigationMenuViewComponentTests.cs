using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using TendaDeportes.Components;
using TendaDeportes.Models;
using Xunit;

namespace TendaDeportes.Tests
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            // Arrange
            Mock<ITendaRepository> mock = new Mock<ITendaRepository>();
            mock.Setup(m => m.Productos).Returns((new Producto[] {
                new Producto {ProductoID = 1, Nome = "P1",
                Categoria = "Mazans"},
                new Producto {ProductoID = 2, Nome = "P2",
                Categoria = "Mazans"},
                new Producto {ProductoID = 3, Nome = "P3",
                Categoria = "Cereixas"},
                new Producto {ProductoID = 4, Nome = "P4",
                Categoria = "Laranxas"},
                }).AsQueryable<Producto>());

            NavigationMenuViewComponent obxectivo =
            new NavigationMenuViewComponent(mock.Object);

            // Act = get o conxunto de categorias
            string[] resultados = ((IEnumerable<string>?)(obxectivo.Invoke()
            as ViewViewComponentResult)?.ViewData?.Model
            ?? Enumerable.Empty<string>()).ToArray();

            // Assert
            Assert.True(Enumerable.SequenceEqual(new string[] { "Mazans", "Laranxas", "Cereixas" }, resultados));
        }

        [Fact]
        public void Indicates_Categoria_Seleccionada()
        {
            // Arrange
            string categoriaASeleccionar = "Mazans";
            Mock<ITendaRepository> mock = new Mock<ITendaRepository>();
            mock.Setup(m => m.Productos).Returns((new Producto[] {
                new Producto {ProductoID = 1, Nome = "P1", Categoria = "Mazans"},
                new Producto {ProductoID = 4, Nome = "P2", Categoria = "Laranxas"},
                }).AsQueryable<Producto>());

            NavigationMenuViewComponent obxectivo =
            new NavigationMenuViewComponent(mock.Object);
            obxectivo.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new ViewContext
                {
                    RouteData = new Microsoft.AspNetCore.Routing.RouteData()
                }
            };
            obxectivo.RouteData.Values["categoria"] = categoriaASeleccionar;
            // Action
            string? resultado = (string?)(obxectivo.Invoke()
            as ViewViewComponentResult)?.ViewData?["SelectedCategory"];

            // Assert
            Assert.Equal(categoriaASeleccionar, resultado);
        }
    }
}