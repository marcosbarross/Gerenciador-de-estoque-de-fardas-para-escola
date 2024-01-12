using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstoqueBackend.Migrations
{
    /// <inheritdoc />
    public partial class adicionaColunasTabelaPedidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedido_Pedido_PedidoId",
                table: "ItemPedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedido",
                table: "Pedido");

            migrationBuilder.RenameTable(
                name: "Pedido",
                newName: "Pedidos");

            migrationBuilder.AddColumn<string>(
                name: "produtoNome",
                table: "Pedidos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "produtoTamanho",
                table: "Pedidos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedido_Pedidos_PedidoId",
                table: "ItemPedido",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedido_Pedidos_PedidoId",
                table: "ItemPedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "produtoNome",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "produtoTamanho",
                table: "Pedidos");

            migrationBuilder.RenameTable(
                name: "Pedidos",
                newName: "Pedido");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedido",
                table: "Pedido",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedido_Pedido_PedidoId",
                table: "ItemPedido",
                column: "PedidoId",
                principalTable: "Pedido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
