using Microsoft.EntityFrameworkCore.Migrations;

namespace BotData.Api.Migrations
{
    public partial class AddedEntranceProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EntranceSound",
                table: "User",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntranceSound",
                table: "User");
        }
    }
}
