using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sports.Migrations
{
    /// <inheritdoc />
    public partial class AddMatchesReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "matchesReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    PlayersReportsId = table.Column<int>(type: "int", nullable: false),
                    CardsReportsId = table.Column<int>(type: "int", nullable: false),
                    GoalsReportsId = table.Column<int>(type: "int", nullable: false),
                    Tech_AdminReportsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matchesReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_matchesReports_CardsReports_CardsReportsId",
                        column: x => x.CardsReportsId,
                        principalTable: "CardsReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_matchesReports_Tech_AdminReports_Tech_AdminReportsId",
                        column: x => x.Tech_AdminReportsId,
                        principalTable: "Tech_AdminReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_matchesReports_goalsReports_GoalsReportsId",
                        column: x => x.GoalsReportsId,
                        principalTable: "goalsReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_matchesReports_matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_matchesReports_playersReports_PlayersReportsId",
                        column: x => x.PlayersReportsId,
                        principalTable: "playersReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_matchesReports_CardsReportsId",
                table: "matchesReports",
                column: "CardsReportsId");

            migrationBuilder.CreateIndex(
                name: "IX_matchesReports_GoalsReportsId",
                table: "matchesReports",
                column: "GoalsReportsId");

            migrationBuilder.CreateIndex(
                name: "IX_matchesReports_MatchId",
                table: "matchesReports",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_matchesReports_PlayersReportsId",
                table: "matchesReports",
                column: "PlayersReportsId");

            migrationBuilder.CreateIndex(
                name: "IX_matchesReports_Tech_AdminReportsId",
                table: "matchesReports",
                column: "Tech_AdminReportsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "matchesReports");
        }
    }
}
