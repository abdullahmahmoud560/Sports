using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sports.Migrations
{
    /// <inheritdoc />
    public partial class updateTableAcademies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Under18",
                table: "Academies",
                newName: "Under12");

            migrationBuilder.RenameColumn(
                name: "Coordinator",
                table: "Academies",
                newName: "AdditionalTShirtColor");

            migrationBuilder.RenameColumn(
                name: "AcademyCity",
                table: "Academies",
                newName: "AdditionalShortColor");

            migrationBuilder.RenameColumn(
                name: "AcademyCall",
                table: "Academies",
                newName: "AdditionalShoesColor");

            migrationBuilder.AlterColumn<bool>(
                name: "Statue",
                table: "Academies",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Academies",
                keyColumn: "Role",
                keyValue: null,
                column: "Role",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Academies",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Academies",
                keyColumn: "AcademyPhone",
                keyValue: null,
                column: "AcademyPhone",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "AcademyPhone",
                table: "Academies",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Academies",
                keyColumn: "AcademyPassword",
                keyValue: null,
                column: "AcademyPassword",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "AcademyPassword",
                table: "Academies",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Academies",
                keyColumn: "AcademyName",
                keyValue: null,
                column: "AcademyName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "AcademyName",
                table: "Academies",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Academies",
                keyColumn: "AcademyEmail",
                keyValue: null,
                column: "AcademyEmail",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "AcademyEmail",
                table: "Academies",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Academies",
                keyColumn: "AcademyCountry",
                keyValue: null,
                column: "AcademyCountry",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "AcademyCountry",
                table: "Academies",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalEmail",
                table: "Academies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalPhoneNumber",
                table: "Academies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ShoesColor",
                table: "Academies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ShortColor",
                table: "Academies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TShirtColor",
                table: "Academies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalEmail",
                table: "Academies");

            migrationBuilder.DropColumn(
                name: "AdditionalPhoneNumber",
                table: "Academies");

            migrationBuilder.DropColumn(
                name: "ShoesColor",
                table: "Academies");

            migrationBuilder.DropColumn(
                name: "ShortColor",
                table: "Academies");

            migrationBuilder.DropColumn(
                name: "TShirtColor",
                table: "Academies");

            migrationBuilder.RenameColumn(
                name: "Under12",
                table: "Academies",
                newName: "Under18");

            migrationBuilder.RenameColumn(
                name: "AdditionalTShirtColor",
                table: "Academies",
                newName: "Coordinator");

            migrationBuilder.RenameColumn(
                name: "AdditionalShortColor",
                table: "Academies",
                newName: "AcademyCity");

            migrationBuilder.RenameColumn(
                name: "AdditionalShoesColor",
                table: "Academies",
                newName: "AcademyCall");

            migrationBuilder.AlterColumn<bool>(
                name: "Statue",
                table: "Academies",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Academies",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "AcademyPhone",
                table: "Academies",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "AcademyPassword",
                table: "Academies",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "AcademyName",
                table: "Academies",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "AcademyEmail",
                table: "Academies",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "AcademyCountry",
                table: "Academies",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
