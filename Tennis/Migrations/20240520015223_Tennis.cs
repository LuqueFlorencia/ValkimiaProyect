using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tennis.Migrations
{
    /// <inheritdoc />
    public partial class Tennis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    IdPerson = table.Column<int>(type: "int", maxLength: 4, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.IdPerson);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    IdPlayer = table.Column<int>(type: "int", maxLength: 4, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPerson = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Hand = table.Column<int>(type: "int", nullable: false),
                    Strength = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    Speed = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    ReactionTime = table.Column<int>(type: "int", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.IdPlayer);
                    table.ForeignKey(
                        name: "FK_Player_Person_IdPerson",
                        column: x => x.IdPerson,
                        principalTable: "Person",
                        principalColumn: "IdPerson",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", maxLength: 4, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPerson = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiration = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_User_Person_IdPerson",
                        column: x => x.IdPerson,
                        principalTable: "Person",
                        principalColumn: "IdPerson",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tournament",
                columns: table => new
                {
                    IdTournament = table.Column<int>(type: "int", maxLength: 4, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Capacity = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    Prize = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    WinnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournament", x => x.IdTournament);
                    table.ForeignKey(
                        name: "FK_Tournament_Player_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Player",
                        principalColumn: "IdPlayer");
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    IdMatch = table.Column<int>(type: "int", maxLength: 4, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPlayer1 = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    IdPlayer2 = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    IdTournament = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    MatchType = table.Column<int>(type: "int", maxLength: 1, nullable: false),
                    WinnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => new { x.IdPlayer1, x.IdPlayer2, x.IdMatch });
                    table.ForeignKey(
                        name: "FK_Match_Player",
                        column: x => x.WinnerId,
                        principalTable: "Player",
                        principalColumn: "IdPlayer");
                    table.ForeignKey(
                        name: "FK_Match_Player1",
                        column: x => x.IdPlayer1,
                        principalTable: "Player",
                        principalColumn: "IdPlayer",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Player2",
                        column: x => x.IdPlayer2,
                        principalTable: "Player",
                        principalColumn: "IdPlayer",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Tournament_IdTournament",
                        column: x => x.IdTournament,
                        principalTable: "Tournament",
                        principalColumn: "IdTournament",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegisteredPlayer",
                columns: table => new
                {
                    TournamentId = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    PlayerId = table.Column<int>(type: "int", maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredPlayer", x => new { x.PlayerId, x.TournamentId });
                    table.ForeignKey(
                        name: "FK_RegisteredPlayer_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "IdPlayer",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegisteredPlayer_Tournament_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournament",
                        principalColumn: "IdTournament",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Match_IdPlayer2",
                table: "Match",
                column: "IdPlayer2");

            migrationBuilder.CreateIndex(
                name: "IX_Match_IdTournament",
                table: "Match",
                column: "IdTournament");

            migrationBuilder.CreateIndex(
                name: "IX_Match_WinnerId",
                table: "Match",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_IdPerson",
                table: "Player",
                column: "IdPerson",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredPlayer_TournamentId",
                table: "RegisteredPlayer",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournament_WinnerId",
                table: "Tournament",
                column: "WinnerId",
                unique: true,
                filter: "[WinnerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_IdPerson",
                table: "User",
                column: "IdPerson",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "RegisteredPlayer");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Tournament");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
