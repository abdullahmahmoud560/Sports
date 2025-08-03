using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sports.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNullableColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_matchesReports_CardsReports_CardsReportsId",
                table: "matchesReports");

            migrationBuilder.DropForeignKey(
                name: "FK_matchesReports_Tech_AdminReports_Tech_AdminReportsId",
                table: "matchesReports");

            migrationBuilder.DropForeignKey(
                name: "FK_matchesReports_academies_AcademyId",
                table: "matchesReports");

            migrationBuilder.DropForeignKey(
                name: "FK_matchesReports_goalsReports_GoalsReportsId",
                table: "matchesReports");

            migrationBuilder.DropForeignKey(
                name: "FK_matchesReports_matches_MatchId",
                table: "matchesReports");

            migrationBuilder.DropForeignKey(
                name: "FK_matchesReports_playersReports_PlayersReportsId",
                table: "matchesReports");

            migrationBuilder.AlterColumn<int>(
                name: "Tech_AdminReportsId",
                table: "matchesReports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PlayersReportsId",
                table: "matchesReports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MatchId",
                table: "matchesReports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GoalsReportsId",
                table: "matchesReports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CardsReportsId",
                table: "matchesReports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AcademyId",
                table: "matchesReports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_matchesReports_CardsReports_CardsReportsId",
                table: "matchesReports",
                column: "CardsReportsId",
                principalTable: "CardsReports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_matchesReports_Tech_AdminReports_Tech_AdminReportsId",
                table: "matchesReports",
                column: "Tech_AdminReportsId",
                principalTable: "Tech_AdminReports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_matchesReports_academies_AcademyId",
                table: "matchesReports",
                column: "AcademyId",
                principalTable: "academies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_matchesReports_goalsReports_GoalsReportsId",
                table: "matchesReports",
                column: "GoalsReportsId",
                principalTable: "goalsReports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_matchesReports_matches_MatchId",
                table: "matchesReports",
                column: "MatchId",
                principalTable: "matches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_matchesReports_playersReports_PlayersReportsId",
                table: "matchesReports",
                column: "PlayersReportsId",
                principalTable: "playersReports",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_matchesReports_CardsReports_CardsReportsId",
                table: "matchesReports");

            migrationBuilder.DropForeignKey(
                name: "FK_matchesReports_Tech_AdminReports_Tech_AdminReportsId",
                table: "matchesReports");

            migrationBuilder.DropForeignKey(
                name: "FK_matchesReports_academies_AcademyId",
                table: "matchesReports");

            migrationBuilder.DropForeignKey(
                name: "FK_matchesReports_goalsReports_GoalsReportsId",
                table: "matchesReports");

            migrationBuilder.DropForeignKey(
                name: "FK_matchesReports_matches_MatchId",
                table: "matchesReports");

            migrationBuilder.DropForeignKey(
                name: "FK_matchesReports_playersReports_PlayersReportsId",
                table: "matchesReports");

            migrationBuilder.AlterColumn<int>(
                name: "Tech_AdminReportsId",
                table: "matchesReports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlayersReportsId",
                table: "matchesReports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MatchId",
                table: "matchesReports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GoalsReportsId",
                table: "matchesReports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CardsReportsId",
                table: "matchesReports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AcademyId",
                table: "matchesReports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_matchesReports_CardsReports_CardsReportsId",
                table: "matchesReports",
                column: "CardsReportsId",
                principalTable: "CardsReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_matchesReports_Tech_AdminReports_Tech_AdminReportsId",
                table: "matchesReports",
                column: "Tech_AdminReportsId",
                principalTable: "Tech_AdminReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_matchesReports_academies_AcademyId",
                table: "matchesReports",
                column: "AcademyId",
                principalTable: "academies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_matchesReports_goalsReports_GoalsReportsId",
                table: "matchesReports",
                column: "GoalsReportsId",
                principalTable: "goalsReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_matchesReports_matches_MatchId",
                table: "matchesReports",
                column: "MatchId",
                principalTable: "matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_matchesReports_playersReports_PlayersReportsId",
                table: "matchesReports",
                column: "PlayersReportsId",
                principalTable: "playersReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
