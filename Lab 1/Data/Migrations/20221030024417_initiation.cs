using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab_1.Data.Migrations
{
    public partial class initiation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "TeamPicture",
                table: "Teams",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isPlayer",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Arenas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arenas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TeamId",
                table: "AspNetUsers",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Teams_TeamId",
                table: "AspNetUsers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Teams_TeamId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Arenas");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TeamId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TeamPicture",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "isPlayer",
                table: "AspNetUsers");
        }
    }
}
