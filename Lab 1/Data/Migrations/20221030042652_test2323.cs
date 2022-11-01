using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab_1.Data.Migrations
{
    public partial class test2323 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamOneID",
                table: "Matches",
                column: "TeamOneID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamTwoID",
                table: "Matches",
                column: "TeamTwoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_TeamOneID",
                table: "Matches",
                column: "TeamOneID",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_TeamTwoID",
                table: "Matches",
                column: "TeamTwoID",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_TeamOneID",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_TeamTwoID",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamOneID",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamTwoID",
                table: "Matches");
        }
    }
}
