using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CurlingTracker.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    CZId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "Linescore",
                columns: table => new
                {
                    LinescoreId = table.Column<Guid>(nullable: false),
                    NumberOfEnds = table.Column<int>(nullable: false),
                    DictionaryAsXml = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linescore", x => x.LinescoreId);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<Guid>(nullable: false),
                    TeamType = table.Column<int>(nullable: false),
                    gender = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "Draws",
                columns: table => new
                {
                    DrawId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    DisplayName = table.Column<string>(nullable: false),
                    EventId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Draws", x => x.DrawId);
                    table.ForeignKey(
                        name: "FK_Draws_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventType",
                columns: table => new
                {
                    EventTypeId = table.Column<Guid>(nullable: false),
                    EventId = table.Column<Guid>(nullable: false),
                    teamType = table.Column<int>(nullable: false),
                    NumberOfPlayers = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    NumberOfEnds = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventType", x => x.EventTypeId);
                    table.ForeignKey(
                        name: "FK_EventType_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    position = table.Column<int>(nullable: false),
                    IsSkip = table.Column<bool>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    TeamId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<Guid>(nullable: false),
                    Team1TeamId = table.Column<Guid>(nullable: false),
                    Team2TeamId = table.Column<Guid>(nullable: false),
                    EventId = table.Column<Guid>(nullable: false),
                    PercentagesAvailable = table.Column<bool>(nullable: false),
                    LinescoreId = table.Column<Guid>(nullable: false),
                    IsFinal = table.Column<bool>(nullable: false),
                    DrawId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Games_Draws_DrawId",
                        column: x => x.DrawId,
                        principalTable: "Draws",
                        principalColumn: "DrawId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Linescore_LinescoreId",
                        column: x => x.LinescoreId,
                        principalTable: "Linescore",
                        principalColumn: "LinescoreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Teams_Team1TeamId",
                        column: x => x.Team1TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Teams_Team2TeamId",
                        column: x => x.Team2TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Draws_EventId",
                table: "Draws",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventType_EventId",
                table: "EventType",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_DrawId",
                table: "Games",
                column: "DrawId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_LinescoreId",
                table: "Games",
                column: "LinescoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Team1TeamId",
                table: "Games",
                column: "Team1TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Team2TeamId",
                table: "Games",
                column: "Team2TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventType");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Draws");

            migrationBuilder.DropTable(
                name: "Linescore");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
