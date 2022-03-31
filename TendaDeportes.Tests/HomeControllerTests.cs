using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TendaDeportes.Controllers;
using TendaDeportes.Models;
using TendaDeportes.Models.ViewModels;
using Xunit;

namespace TendaDeportes.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Pode_Usar_Repository()
        {
            //Arrange
            Mock<ITendaRepository> mock = new Mock<ITendaRepository>();
            mock.Setup(m => m.Productos).Returns((new Producto[]
            {
                new Producto {ProductoID=1,Nome="P1"},
                new Producto{ProductoID=2,Nome="P2"}
            }).AsQueryable<Producto>());

            HomeController controller = new HomeController(mock.Object);

            //Act
            // IEnumerable<Producto>? resultado =
            // (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Producto>;
            ProductosListaViewModel resultado = controller.Index(null)?.ViewData.Model as ProductosListaViewModel ?? new();

            //Assert
            // Producto[] productoArray = resultado?.ToArray() ?? Array.Empty<Producto>();
            Producto[] productoArray = resultado.Productos.ToArray();
            Assert.True(productoArray.Length == 2);
            Assert.Equal("P1", productoArray[0].Nome);
            Assert.Equal("P2", productoArray[1].Nome);
        }

        [Fact]
        public void Pode_Paxinar()
        {
            //Arrange
            Mock<ITendaRepository> mock = new Mock<ITendaRepository>();
            mock.Setup(m => m.Productos).Returns((new Producto[]
            {
                new Producto {ProductoID = 1, Nome = "P1"},
                new Producto {ProductoID = 2, Nome = "P2"},
                new Producto {ProductoID = 3, Nome = "P3"},
                new Producto {ProductoID = 4, Nome = "P4"},
                new Producto {ProductoID = 5, Nome = "P5"}
            }
            ).AsQueryable<Producto>());

            HomeController controller = new HomeController(mock.Object);
            controller.TamanhoPaxina = 3;

            //Act
            ProductosListaViewModel resultado = controller.Index(null, 2)?.ViewData.Model as ProductosListaViewModel ?? new();

            //Assert
            Producto[] prodArray = resultado.Productos.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Nome);
            Assert.Equal("P5", prodArray[1].Nome);
        }

        [Fact]
        public void Can_Enviar_Paxinacion_View_Model()
        {
            //Arrange
            Mock<ITendaRepository> mock = new Mock<ITendaRepository>();
            mock.Setup(m => m.Productos).Returns((new Producto[] {
                new Producto {ProductoID = 1, Nome = "P1"},
                new Producto {ProductoID = 2, Nome = "P2"},
                new Producto {ProductoID = 3, Nome = "P3"},
                new Producto {ProductoID = 4, Nome = "P4"},
                new Producto {ProductoID = 5, Nome = "P5"}
            }).AsQueryable<Producto>());

            //Arrange
            HomeController controller = new HomeController(mock.Object) { TamanhoPaxina = 3 };

            //Act
            ProductosListaViewModel resultado = controller.Index(null, 2)?.ViewData.Model as ProductosListaViewModel ?? new();

            //Assert
            PaxinacionInfo paxinacionInfo = resultado.PaxinacionInfo;
            Assert.Equal(2, paxinacionInfo.PaxinaActual);
            Assert.Equal(3, paxinacionInfo.PaxinaActual);
            Assert.Equal(5, paxinacionInfo.PaxinaActual);
            Assert.Equal(2, paxinacionInfo.PaxinaActual);
        }

        [Fact]
        public void Can_Filtrar_Productos()
        {
            //Arrange
            // -crea un repositorio mock
            Mock<ITendaRepository> mock = new Mock<ITendaRepository>();

            mock.Setup(m => m.Productos).Returns((new Producto[] {
                new Producto {ProductoID = 1, Nome = "P1", Categoria = "Cat1"},
                new Producto {ProductoID = 2, Nome = "P2", Categoria = "Cat2"},
                new Producto {ProductoID = 3, Nome = "P3", Categoria = "Cat1"},
                new Producto {ProductoID = 4, Nome = "P4", Categoria = "Cat2"},
                new Producto {ProductoID = 5, Nome = "P5", Categoria = "Cat3"}
                }).AsQueryable<Producto>());

            // Arrange - crea un controller e fai que o tamanho de paxina sexa de 3 elementos
            HomeController controller = new HomeController(mock.Object);
            controller.TamanhoPaxina = 3;
            // Action
            Producto[] resultado = (controller.Index("Cat2", 1)?.ViewData.Model
            as ProductosListaViewModel ?? new()).Productos.ToArray();
            // Assert
            Assert.Equal(2, resultado.Length);
            Assert.True(resultado[0].Nome == "P2" && resultado[0].Categoria == "Cat2");
            Assert.True(resultado[1].Nome == "P4" && resultado[1].Categoria == "Cat2");
        }

        [Fact]
        public void Generate_Categoria_Producto_Especifico_Count()
        {
            // Arrange
            Mock<ITendaRepository> mock = new Mock<ITendaRepository>();
            mock.Setup(m => m.Productos).Returns((new Producto[] {
                new Producto {ProductoID = 1, Nome = "P1", Categoria = "Cat1"},
                new Producto {ProductoID = 2, Nome = "P2", Categoria = "Cat2"},
                new Producto {ProductoID = 3, Nome = "P3", Categoria = "Cat1"},
                new Producto {ProductoID = 4, Nome = "P4", Categoria = "Cat2"},
                new Producto {ProductoID = 5, Nome = "P5", Categoria = "Cat3"}
                }).AsQueryable<Producto>());

            HomeController obxectivo = new HomeController(mock.Object);
            obxectivo.TamanhoPaxina = 3;

            Func<ViewResult, ProductosListaViewModel?> GetModel = result
            => result?.ViewData?.Model as ProductosListaViewModel;

            // Action
            int? res1 = GetModel(obxectivo.Index("Cat1"))?.PaxinacionInfo.TotalItems;
            int? res2 = GetModel(obxectivo.Index("Cat2"))?.PaxinacionInfo.TotalItems;
            int? res3 = GetModel(obxectivo.Index("Cat3"))?.PaxinacionInfo.TotalItems;
            int? resTodas = GetModel(obxectivo.Index(null))?.PaxinacionInfo.TotalItems;
            // Assert
            Assert.Equal(2, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(5, resTodas);
        }
    }
}