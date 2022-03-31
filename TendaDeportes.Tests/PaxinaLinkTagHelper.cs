using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using TendaDeportes.Infrastructura;
using TendaDeportes.Models.ViewModels;
using Xunit;

namespace TendaDeportes.Tests
{
    public class PaxinaLinkTagHelperTests
    {
        [Fact]
        public void Can_Xenerar_Paxina_Links()
        {
            //Arrange
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("Test/Paxina1")
                .Returns("Test/Paxina2")
                .Returns("Test/Paxina3");

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(f => f.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(urlHelper.Object);

            var viewContext = new Mock<ViewContext>();

            PaxinaLinkTagHelper helper =
                new PaxinaLinkTagHelper(urlHelperFactory.Object)
                {
                    PageModel = new PaxinacionInfo
                    {
                        PaxinaActual = 2,
                        TotalItems = 28,
                        ItemsPorPaxina = 10
                    },
                    ViewContext = viewContext.Object,
                    PageAction = "Test"
                };

            TagHelperContext ctx = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(), "");

            var contido = new Mock<TagHelperContent>();
            TagHelperOutput output = new TagHelperOutput("div", new TagHelperAttributeList(),
            (cache, encoder) => Task.FromResult(contido.Object));

            //Act
            helper.Process(ctx, output);

            //Assert
            Assert.Equal(@"<a href=""Test/Paxina1"">1</a>"
                + @"<a href=""Test/Paxina2"">2</a>"
                + @"<a href=""Test/Paxina3"">3</a>",
                output.Content.GetContent());
        }
    }
}