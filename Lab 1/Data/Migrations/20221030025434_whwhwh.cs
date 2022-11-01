using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab_1.Data.Migrations
{
    public partial class whwhwh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamOneID",
                table: "Matches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamOneScore",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamTwoID",
                table: "Matches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamTwoScore",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.DropColumn(
                name: "TeamOneID",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "TeamOneScore",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "TeamTwoID",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "TeamTwoScore",
                table: "Matches");
        }
    }
}
