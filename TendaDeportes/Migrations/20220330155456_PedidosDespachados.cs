using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TendaDeportes.Migrations
{
    public partial class PedidosDespachados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Despachado",
                table: "Pedidos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Despachado",
                table: "Pedidos");
        }
    }
}
