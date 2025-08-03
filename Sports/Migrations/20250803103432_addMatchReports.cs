using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sports.Migrations
{
    /// <inheritdoc />
    public partial class addMatchReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "matchesReports");

            migrationBuilder.DropTable(
                name: "Tech_AdminReports");

            migrationBuilder.RenameColumn(
                name: "Possition",
                table: "playersReports",
                newName: "Position");

            migrationBuilder.AddColumn<int>(
                name: "MatchId",
                table: "playersReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MatchId",
                table: "goalsReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MatchId",
                table: "CardsReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StaffReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TechName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffReport_matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_StaffReport_MatchId",
                table: "StaffReport",
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

            migrationBuilder.DropTable(
                name: "StaffReport");

            migrationBuilder.DropIndex(
                name: "IX_playersReports_MatchId",
                table: "playersReports");

            migrationBuilder.DropIndex(
                name: "IX_goalsReports_MatchId",
                table: "goalsReports");

            migrationBuilder.DropIndex(
                name: "IX_CardsReports_MatchId",
                table: "CardsReports");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "playersReports");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "goalsReports");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "CardsReports");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "playersReports",
                newName: "Possition");

            migrationBuilder.CreateTable(
                name: "Tech_AdminReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TechName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    attribute = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tech_AdminReports", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "matchesReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AcademyId = table.Column<int>(type: "int", nullable: true),
                    CardsReportsId = table.Column<int>(type: "int", nullable: true),
                    GoalsReportsId = table.Column<int>(type: "int", nullable: true),
                    MatchId = table.Column<int>(type: "int", nullable: true),
                    PlayersReportsId = table.Column<int>(type: "int", nullable: true),
                    Tech_AdminReportsId = table.Column<int>(type: "int", nullable: true),
                    Catagory = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matchesReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_matchesReports_CardsReports_CardsReportsId",
                        column: x => x.CardsReportsId,
                        principalTable: "CardsReports",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_matchesReports_Tech_AdminReports_Tech_AdminReportsId",
                        column: x => x.Tech_AdminReportsId,
                        principalTable: "Tech_AdminReports",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_matchesReports_academies_AcademyId",
                        column: x => x.AcademyId,
                        principalTable: "academies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_matchesReports_goalsReports_GoalsReportsId",
                        column: x => x.GoalsReportsId,
                        principalTable: "goalsReports",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_matchesReports_matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "matches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_matchesReports_playersReports_PlayersReportsId",
                        column: x => x.PlayersReportsId,
                        principalTable: "playersReports",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_matchesReports_AcademyId",
                table: "matchesReports",
                column: "AcademyId");

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
    }
}
