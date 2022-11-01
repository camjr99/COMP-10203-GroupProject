using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab_1.Data.Migrations
{
    public partial class teams_scoring_ties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ties",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ties",
                table: "Teams");
        }
    }
}
