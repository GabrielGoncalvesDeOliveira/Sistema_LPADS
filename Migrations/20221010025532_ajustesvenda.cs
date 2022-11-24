using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestaoCantinasIgrejas.Migrations
{
    public partial class ajustesvenda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "quantidade",
                table: "Produto",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "quantidade",
                table: "Produto",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
