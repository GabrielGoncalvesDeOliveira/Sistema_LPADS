using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestaoCantinasIgrejas.Migrations
{
    public partial class eventoigreja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "igrejaid",
                table: "Evento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Evento_igrejaid",
                table: "Evento",
                column: "igrejaid");

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_Igreja_igrejaid",
                table: "Evento",
                column: "igrejaid",
                principalTable: "Igreja",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evento_Igreja_igrejaid",
                table: "Evento");

            migrationBuilder.DropIndex(
                name: "IX_Evento_igrejaid",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "igrejaid",
                table: "Evento");
        }
    }
}
