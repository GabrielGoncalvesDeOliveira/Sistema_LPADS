using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestaoCantinasIgrejas.Migrations
{
    public partial class vendasrelacionamentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "idProduto",
                table: "Venda",
                newName: "produtoid");

            migrationBuilder.RenameColumn(
                name: "idParticipante",
                table: "Venda",
                newName: "participanteid");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_participanteid",
                table: "Venda",
                column: "participanteid");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_produtoid",
                table: "Venda",
                column: "produtoid");

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Participante_participanteid",
                table: "Venda",
                column: "participanteid",
                principalTable: "Participante",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Produto_produtoid",
                table: "Venda",
                column: "produtoid",
                principalTable: "Produto",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Participante_participanteid",
                table: "Venda");

            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Produto_produtoid",
                table: "Venda");

            migrationBuilder.DropIndex(
                name: "IX_Venda_participanteid",
                table: "Venda");

            migrationBuilder.DropIndex(
                name: "IX_Venda_produtoid",
                table: "Venda");

            migrationBuilder.RenameColumn(
                name: "produtoid",
                table: "Venda",
                newName: "idProduto");

            migrationBuilder.RenameColumn(
                name: "participanteid",
                table: "Venda",
                newName: "idParticipante");
        }
    }
}
