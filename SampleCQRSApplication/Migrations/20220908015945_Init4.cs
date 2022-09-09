using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleCQRSApplication.Migrations
{
    public partial class Init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_TeamAId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_TeamBId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamAId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamBId",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "TeamBId",
                table: "Matches",
                newName: "ScoreHomeTeam");

            migrationBuilder.RenameColumn(
                name: "TeamAId",
                table: "Matches",
                newName: "ScoreAwayTeam");

            migrationBuilder.RenameColumn(
                name: "ScoreTeamB",
                table: "Matches",
                newName: "HomeTeamId");

            migrationBuilder.RenameColumn(
                name: "ScoreTeamA",
                table: "Matches",
                newName: "AwayTeamId");

            migrationBuilder.RenameColumn(
                name: "RateTeamB",
                table: "Matches",
                newName: "RateHomeTeam");

            migrationBuilder.RenameColumn(
                name: "RateTeamA",
                table: "Matches",
                newName: "RateAwayTeam");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_AwayTeamId",
                table: "Matches",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HomeTeamId",
                table: "Matches",
                column: "HomeTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_AwayTeamId",
                table: "Matches",
                column: "AwayTeamId",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_HomeTeamId",
                table: "Matches",
                column: "HomeTeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_AwayTeamId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_HomeTeamId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_AwayTeamId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_HomeTeamId",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "ScoreHomeTeam",
                table: "Matches",
                newName: "TeamBId");

            migrationBuilder.RenameColumn(
                name: "ScoreAwayTeam",
                table: "Matches",
                newName: "TeamAId");

            migrationBuilder.RenameColumn(
                name: "RateHomeTeam",
                table: "Matches",
                newName: "RateTeamB");

            migrationBuilder.RenameColumn(
                name: "RateAwayTeam",
                table: "Matches",
                newName: "RateTeamA");

            migrationBuilder.RenameColumn(
                name: "HomeTeamId",
                table: "Matches",
                newName: "ScoreTeamB");

            migrationBuilder.RenameColumn(
                name: "AwayTeamId",
                table: "Matches",
                newName: "ScoreTeamA");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamAId",
                table: "Matches",
                column: "TeamAId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamBId",
                table: "Matches",
                column: "TeamBId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_TeamAId",
                table: "Matches",
                column: "TeamAId",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_TeamBId",
                table: "Matches",
                column: "TeamBId",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
