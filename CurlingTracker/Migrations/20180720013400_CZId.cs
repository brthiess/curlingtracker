using Microsoft.EntityFrameworkCore.Migrations;

namespace CurlingTracker.Migrations
{
    public partial class CZId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CZId",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CZId",
                table: "Events");
        }
    }
}
