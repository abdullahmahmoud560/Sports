using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sports.Migrations
{
    /// <inheritdoc />
    public partial class updateTablePlayersagain1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Under12",
                table: "Academies");

            migrationBuilder.DropColumn(
                name: "Under14",
                table: "Academies");

            migrationBuilder.DropColumn(
                name: "Under16",
                table: "Academies");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Academies");

            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "Players",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category",
                table: "Players");

            migrationBuilder.AddColumn<bool>(
                name: "Under12",
                table: "Academies",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Under14",
                table: "Academies",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Under16",
                table: "Academies",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Academies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
