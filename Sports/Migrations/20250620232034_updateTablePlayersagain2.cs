using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sports.Migrations
{
    /// <inheritdoc />
    public partial class updateTablePlayersagain2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "under12",
                table: "Academies",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "under14",
                table: "Academies",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "under16",
                table: "Academies",
                type: "tinyint(1)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "under12",
                table: "Academies");

            migrationBuilder.DropColumn(
                name: "under14",
                table: "Academies");

            migrationBuilder.DropColumn(
                name: "under16",
                table: "Academies");
        }
    }
}
