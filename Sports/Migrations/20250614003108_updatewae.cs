using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sports.Migrations
{
    /// <inheritdoc />
    public partial class updatewae : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AcademyCall",
                table: "Academies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.AddColumn<bool>(
                name: "Under18",
                table: "Academies",
                type: "tinyint(1)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcademyCall",
                table: "Academies");

            migrationBuilder.DropColumn(
                name: "Under14",
                table: "Academies");

            migrationBuilder.DropColumn(
                name: "Under16",
                table: "Academies");

            migrationBuilder.DropColumn(
                name: "Under18",
                table: "Academies");
        }
    }
}
