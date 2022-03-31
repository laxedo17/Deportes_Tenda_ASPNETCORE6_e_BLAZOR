using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TendaDeportes.Migrations
{
    public partial class Pedidos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    PedidoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Linea1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Linea2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Linea3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lugar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provincia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnvoltorioRegalo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.PedidoID);
                });

            migrationBuilder.CreateTable(
                name: "CestaLinea",
                columns: table => new
                {
                    CestaLineaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoID = table.Column<long>(type: "bigint", nullable: false),
                    Cantidade = table.Column<int>(type: "int", nullable: false),
                    PedidoID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CestaLinea", x => x.CestaLineaID);
                    table.ForeignKey(
                        name: "FK_CestaLinea_Pedidos_PedidoID",
                        column: x => x.PedidoID,
                        principalTable: "Pedidos",
                        principalColumn: "PedidoID");
                    table.ForeignKey(
                        name: "FK_CestaLinea_Productos_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "Productos",
                        principalColumn: "ProductoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CestaLinea_PedidoID",
                table: "CestaLinea",
                column: "PedidoID");

            migrationBuilder.CreateIndex(
                name: "IX_CestaLinea_ProductoID",
                table: "CestaLinea",
                column: "ProductoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CestaLinea");

            migrationBuilder.DropTable(
                name: "Pedidos");
        }
    }
}
