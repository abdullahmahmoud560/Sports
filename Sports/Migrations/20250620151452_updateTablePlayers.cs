using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sports.Migrations
{
    /// <inheritdoc />
    public partial class updateTablePlayers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcademyName",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Goals",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "RedCards",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "YellowCards",
                table: "Players");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PLayerName",
                keyValue: null,
                column: "PLayerName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "PLayerName",
                table: "Players",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "NumberShirt",
                keyValue: null,
                column: "NumberShirt",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "NumberShirt",
                table: "Players",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Nationality",
                keyValue: null,
                column: "Nationality",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Nationality",
                table: "Players",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "URLImage",
                table: "Players",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "URLPassport",
                table: "Players",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URLImage",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "URLPassport",
                table: "Players");

            migrationBuilder.AlterColumn<string>(
                name: "PLayerName",
                table: "Players",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "NumberShirt",
                table: "Players",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Nationality",
                table: "Players",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AcademyName",
                table: "Players",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Players",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Goals",
                table: "Players",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RedCards",
                table: "Players",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "YellowCards",
                table: "Players",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
