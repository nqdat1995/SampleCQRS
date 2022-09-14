using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleCQRSApplication.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bets_Teams_TeamId",
                table: "Bets");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Bets",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bets_TeamId",
                table: "Bets",
                newName: "IX_Bets_UserId");

            migrationBuilder.AddColumn<int>(
                name: "Decision",
                table: "Bets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Bets_Users_UserId",
                table: "Bets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bets_Users_UserId",
                table: "Bets");

            migrationBuilder.DropColumn(
                name: "Decision",
                table: "Bets");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Bets",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Bets_UserId",
                table: "Bets",
                newName: "IX_Bets_TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bets_Teams_TeamId",
                table: "Bets",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
