using Microsoft.EntityFrameworkCore.Migrations;

namespace CurlingTracker.Migrations
{
    public partial class IsOverAndFullyParsed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOverAndFullyParsed",
                table: "Games",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOverAndFullyParsed",
                table: "Events",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOverAndFullyParsed",
                table: "Draws",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOverAndFullyParsed",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "IsOverAndFullyParsed",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "IsOverAndFullyParsed",
                table: "Draws");
        }
    }
}
