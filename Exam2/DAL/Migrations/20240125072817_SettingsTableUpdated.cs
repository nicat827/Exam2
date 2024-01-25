using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam2.DAL.Migrations
{
    public partial class SettingsTableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Setting",
                table: "Setting");

            migrationBuilder.RenameTable(
                name: "Setting",
                newName: "Settings");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Settings",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Settings",
                table: "Settings",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Settings",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Settings");

            migrationBuilder.RenameTable(
                name: "Settings",
                newName: "Setting");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "Setting",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Setting",
                table: "Setting",
                column: "Key");
        }
    }
}
