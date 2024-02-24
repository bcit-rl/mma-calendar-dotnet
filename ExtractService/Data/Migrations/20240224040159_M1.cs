using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace ExtractService.Data.Migrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Fighters",
                columns: table => new
                {
                    FighterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "longtext", nullable: true),
                    LastName = table.Column<string>(type: "longtext", nullable: true),
                    NickName = table.Column<string>(type: "longtext", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<string>(type: "longtext", nullable: true),
                    Citizenship = table.Column<string>(type: "longtext", nullable: true),
                    Headshot = table.Column<string>(type: "longtext", nullable: true),
                    Wins = table.Column<int>(type: "int", nullable: true),
                    Losses = table.Column<int>(type: "int", nullable: true),
                    Draws = table.Column<int>(type: "int", nullable: true),
                    NoContests = table.Column<int>(type: "int", nullable: true),
                    LeftStance = table.Column<string>(type: "longtext", nullable: true),
                    RightStance = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fighters", x => x.FighterId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    VenueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    City = table.Column<string>(type: "longtext", nullable: true),
                    State = table.Column<string>(type: "longtext", nullable: true),
                    Country = table.Column<string>(type: "longtext", nullable: true),
                    Indoor = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.VenueId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    EventName = table.Column<string>(type: "longtext", nullable: true),
                    ShortName = table.Column<string>(type: "longtext", nullable: true),
                    EventDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EventLocationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_Venues_EventLocationId",
                        column: x => x.EventLocationId,
                        principalTable: "Venues",
                        principalColumn: "VenueId");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Fights",
                columns: table => new
                {
                    FightId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DisplayClock = table.Column<string>(type: "longtext", nullable: true),
                    Round = table.Column<int>(type: "int", nullable: true),
                    Method = table.Column<string>(type: "longtext", nullable: true),
                    MethodDescription = table.Column<string>(type: "longtext", nullable: true),
                    CardSegment = table.Column<string>(type: "longtext", nullable: true),
                    Winner = table.Column<string>(type: "longtext", nullable: true),
                    VenueId = table.Column<int>(type: "int", nullable: true),
                    FighterAId = table.Column<int>(type: "int", nullable: true),
                    FighterBId = table.Column<int>(type: "int", nullable: true),
                    EventId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fights", x => x.FightId);
                    table.ForeignKey(
                        name: "FK_Fights_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId");
                    table.ForeignKey(
                        name: "FK_Fights_Fighters_FighterAId",
                        column: x => x.FighterAId,
                        principalTable: "Fighters",
                        principalColumn: "FighterId");
                    table.ForeignKey(
                        name: "FK_Fights_Fighters_FighterBId",
                        column: x => x.FighterBId,
                        principalTable: "Fighters",
                        principalColumn: "FighterId");
                    table.ForeignKey(
                        name: "FK_Fights_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "VenueId");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventLocationId",
                table: "Events",
                column: "EventLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Fights_EventId",
                table: "Fights",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Fights_FighterAId",
                table: "Fights",
                column: "FighterAId");

            migrationBuilder.CreateIndex(
                name: "IX_Fights_FighterBId",
                table: "Fights",
                column: "FighterBId");

            migrationBuilder.CreateIndex(
                name: "IX_Fights_VenueId",
                table: "Fights",
                column: "VenueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fights");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Fighters");

            migrationBuilder.DropTable(
                name: "Venues");
        }
    }
}
