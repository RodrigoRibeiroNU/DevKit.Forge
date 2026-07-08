using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevKit.Forge.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMetricasLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QtdAvisos",
                table: "AnalisesDeLog",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QtdErros",
                table: "AnalisesDeLog",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtdAvisos",
                table: "AnalisesDeLog");

            migrationBuilder.DropColumn(
                name: "QtdErros",
                table: "AnalisesDeLog");
        }
    }
}
