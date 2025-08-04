using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sports.Migrations
{
    /// <inheritdoc />
    public partial class TeamId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_players_academies_AcademyId",
                table: "players");

            migrationBuilder.RenameColumn(
                name: "AcademyId",
                table: "players",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_players_AcademyId",
                table: "players",
                newName: "IX_players_TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_players_teams_TeamId",
                table: "players",
                column: "TeamId",
                principalTable: "teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_players_teams_TeamId",
                table: "players");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "players",
                newName: "AcademyId");

            migrationBuilder.RenameIndex(
                name: "IX_players_TeamId",
                table: "players",
                newName: "IX_players_AcademyId");

            migrationBuilder.AddForeignKey(
                name: "FK_players_academies_AcademyId",
                table: "players",
                column: "AcademyId",
                principalTable: "academies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
