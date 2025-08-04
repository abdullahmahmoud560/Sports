using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sports.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerRelationToGoalsReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "goalsReports",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_goalsReports_PlayerId",
                table: "goalsReports",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_goalsReports_players_PlayerId",
                table: "goalsReports",
                column: "PlayerId",
                principalTable: "players",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_goalsReports_players_PlayerId",
                table: "goalsReports");

            migrationBuilder.DropIndex(
                name: "IX_goalsReports_PlayerId",
                table: "goalsReports");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "goalsReports");
        }
    }
}
