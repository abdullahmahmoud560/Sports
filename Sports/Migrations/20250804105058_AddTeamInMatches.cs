using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sports.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamInMatches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_matches_academies_AwayTeamId",
                table: "matches");

            migrationBuilder.DropForeignKey(
                name: "FK_matches_academies_HomeTeamId",
                table: "matches");

            migrationBuilder.AddForeignKey(
                name: "FK_matches_teams_AwayTeamId",
                table: "matches",
                column: "AwayTeamId",
                principalTable: "teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_matches_teams_HomeTeamId",
                table: "matches",
                column: "HomeTeamId",
                principalTable: "teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_matches_teams_AwayTeamId",
                table: "matches");

            migrationBuilder.DropForeignKey(
                name: "FK_matches_teams_HomeTeamId",
                table: "matches");

            migrationBuilder.AddForeignKey(
                name: "FK_matches_academies_AwayTeamId",
                table: "matches",
                column: "AwayTeamId",
                principalTable: "academies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_matches_academies_HomeTeamId",
                table: "matches",
                column: "HomeTeamId",
                principalTable: "academies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
