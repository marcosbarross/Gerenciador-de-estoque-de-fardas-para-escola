using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstoqueBackend.Migrations
{
    /// <inheritdoc />
    public partial class AjustaColunasCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "CPF",
                table: "Clientes",
                newName: "Aluno");

            migrationBuilder.AddColumn<int>(
                name: "Tamanho",
                table: "Produtos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tamanho",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "Aluno",
                table: "Clientes",
                newName: "CPF");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Produtos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
