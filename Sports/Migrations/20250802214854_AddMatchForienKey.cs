using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sports.Migrations
{
    /// <inheritdoc />
    public partial class AddMatchForienKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tech_AdminReports_MatchId",
                table: "Tech_AdminReports",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_playersReports_MatchId",
                table: "playersReports",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_goalsReports_MatchId",
                table: "goalsReports",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_CardsReports_MatchId",
                table: "CardsReports",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardsReports_matches_MatchId",
                table: "CardsReports",
                column: "MatchId",
                principalTable: "matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_goalsReports_matches_MatchId",
                table: "goalsReports",
                column: "MatchId",
                principalTable: "matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_playersReports_matches_MatchId",
                table: "playersReports",
                column: "MatchId",
                principalTable: "matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tech_AdminReports_matches_MatchId",
                table: "Tech_AdminReports",
                column: "MatchId",
                principalTable: "matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardsReports_matches_MatchId",
                table: "CardsReports");

            migrationBuilder.DropForeignKey(
                name: "FK_goalsReports_matches_MatchId",
                table: "goalsReports");

            migrationBuilder.DropForeignKey(
                name: "FK_playersReports_matches_MatchId",
                table: "playersReports");

            migrationBuilder.DropForeignKey(
                name: "FK_Tech_AdminReports_matches_MatchId",
                table: "Tech_AdminReports");

            migrationBuilder.DropIndex(
                name: "IX_Tech_AdminReports_MatchId",
                table: "Tech_AdminReports");

            migrationBuilder.DropIndex(
                name: "IX_playersReports_MatchId",
                table: "playersReports");

            migrationBuilder.DropIndex(
                name: "IX_goalsReports_MatchId",
                table: "goalsReports");

            migrationBuilder.DropIndex(
                name: "IX_CardsReports_MatchId",
                table: "CardsReports");
        }
    }
}
