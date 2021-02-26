using Microsoft.EntityFrameworkCore.Migrations;

namespace BotData.Data.Migrations
{
    public partial class AddCorrectCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Correct",
                table: "GuessGameAttempt",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Correct",
                table: "GuessGameAttempt");
        }
    }
}
