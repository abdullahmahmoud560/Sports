using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sports.Migrations
{
    /// <inheritdoc />
    public partial class AddAcademyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AcademyId",
                table: "matchesReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_matchesReports_AcademyId",
                table: "matchesReports",
                column: "AcademyId");

            migrationBuilder.AddForeignKey(
                name: "FK_matchesReports_academies_AcademyId",
                table: "matchesReports",
                column: "AcademyId",
                principalTable: "academies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_matchesReports_academies_AcademyId",
                table: "matchesReports");

            migrationBuilder.DropIndex(
                name: "IX_matchesReports_AcademyId",
                table: "matchesReports");

            migrationBuilder.DropColumn(
                name: "AcademyId",
                table: "matchesReports");
        }
    }
}
