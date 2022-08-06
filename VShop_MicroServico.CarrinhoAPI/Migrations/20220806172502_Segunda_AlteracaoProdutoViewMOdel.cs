using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VShop_MicroServico.CarrinhoAPI.Migrations
{
    public partial class Segunda_AlteracaoProdutoViewMOdel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoItems_CarrinhoCabecs_CarrinhoCabecId",
                table: "CarrinhoItems");

            migrationBuilder.DropIndex(
                name: "IX_CarrinhoItems_CarrinhoCabecId",
                table: "CarrinhoItems");

            migrationBuilder.RenameColumn(
                name: "ImageURL",
                table: "Produtos",
                newName: "ImagemURL");

            migrationBuilder.RenameColumn(
                name: "CategoryNome",
                table: "Produtos",
                newName: "CategoriaNome");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagemURL",
                table: "Produtos",
                newName: "ImageURL");

            migrationBuilder.RenameColumn(
                name: "CategoriaNome",
                table: "Produtos",
                newName: "CategoryNome");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoItems_CarrinhoCabecId",
                table: "CarrinhoItems",
                column: "CarrinhoCabecId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoItems_CarrinhoCabecs_CarrinhoCabecId",
                table: "CarrinhoItems",
                column: "CarrinhoCabecId",
                principalTable: "CarrinhoCabecs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
