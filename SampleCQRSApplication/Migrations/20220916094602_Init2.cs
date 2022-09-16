using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleCQRSApplication.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_TournamentRounds_RoundId",
                table: "Rounds");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_TournamentRounds_TournamentId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_TournamentId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Rounds_RoundId",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "RoundId",
                table: "Rounds");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRounds_RoundId",
                table: "TournamentRounds",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRounds_TournamentId",
                table: "TournamentRounds",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRounds_Rounds_RoundId",
                table: "TournamentRounds",
                column: "RoundId",
                principalTable: "Rounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRounds_Tournaments_TournamentId",
                table: "TournamentRounds",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRounds_Rounds_RoundId",
                table: "TournamentRounds");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRounds_Tournaments_TournamentId",
                table: "TournamentRounds");

            migrationBuilder.DropIndex(
                name: "IX_TournamentRounds_RoundId",
                table: "TournamentRounds");

            migrationBuilder.DropIndex(
                name: "IX_TournamentRounds_TournamentId",
                table: "TournamentRounds");

            migrationBuilder.AddColumn<int>(
                name: "TournamentId",
                table: "Tournaments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoundId",
                table: "Rounds",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_TournamentId",
                table: "Tournaments",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_RoundId",
                table: "Rounds",
                column: "RoundId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rounds_TournamentRounds_RoundId",
                table: "Rounds",
                column: "RoundId",
                principalTable: "TournamentRounds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_TournamentRounds_TournamentId",
                table: "Tournaments",
                column: "TournamentId",
                principalTable: "TournamentRounds",
                principalColumn: "Id");
        }
    }
}
