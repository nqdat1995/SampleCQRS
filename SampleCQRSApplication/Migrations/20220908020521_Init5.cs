using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleCQRSApplication.Migrations
{
    public partial class Init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Round_RoundId",
                table: "Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Round",
                table: "Round");

            migrationBuilder.RenameTable(
                name: "Round",
                newName: "Rounds");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Teams",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoundId",
                table: "Rounds",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rounds",
                table: "Rounds",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Bets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bets_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bets_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TournamentRounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    RoundId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentRounds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TournamentSeasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    SeasonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentSeasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TournamentTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentTeams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeasonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seasons_TournamentSeasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "TournamentSeasons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TournamentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tournaments_TournamentRounds_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "TournamentRounds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tournaments_TournamentSeasons_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "TournamentSeasons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tournaments_TournamentTeams_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "TournamentTeams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamId",
                table: "Teams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_RoundId",
                table: "Rounds",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_MatchId",
                table: "Bets",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_TeamId",
                table: "Bets",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_SeasonId",
                table: "Seasons",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_TournamentId",
                table: "Tournaments",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Rounds_RoundId",
                table: "Matches",
                column: "RoundId",
                principalTable: "Rounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rounds_TournamentRounds_RoundId",
                table: "Rounds",
                column: "RoundId",
                principalTable: "TournamentRounds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_TournamentTeams_TeamId",
                table: "Teams",
                column: "TeamId",
                principalTable: "TournamentTeams",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Rounds_RoundId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_TournamentRounds_RoundId",
                table: "Rounds");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_TournamentTeams_TeamId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "Bets");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "TournamentRounds");

            migrationBuilder.DropTable(
                name: "TournamentSeasons");

            migrationBuilder.DropTable(
                name: "TournamentTeams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamId",
                table: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rounds",
                table: "Rounds");

            migrationBuilder.DropIndex(
                name: "IX_Rounds_RoundId",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "RoundId",
                table: "Rounds");

            migrationBuilder.RenameTable(
                name: "Rounds",
                newName: "Round");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Round",
                table: "Round",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Round_RoundId",
                table: "Matches",
                column: "RoundId",
                principalTable: "Round",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
