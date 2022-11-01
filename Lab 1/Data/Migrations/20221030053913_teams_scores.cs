using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab_1.Data.Migrations
{
    public partial class teams_scores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Losses",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Wins",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArenaID",
                table: "Matches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_ArenaID",
                table: "Matches",
                column: "ArenaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Arenas_ArenaID",
                table: "Matches",
                column: "ArenaID",
                principalTable: "Arenas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Arenas_ArenaID",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_ArenaID",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Losses",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Wins",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ArenaID",
                table: "Matches");
        }
    }
}
