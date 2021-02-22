using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BotData.Data.Migrations
{
    public partial class MigrateUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuessGame_User_UserId",
                table: "GuessGame");

            migrationBuilder.DropForeignKey(
                name: "FK_GuessGameAttempt_User_UserId",
                table: "GuessGameAttempt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_DiscordId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_GuessGameAttempt_UserId",
                table: "GuessGameAttempt");

            migrationBuilder.DropIndex(
                name: "IX_GuessGame_UserId",
                table: "GuessGame");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GuessGameAttempt");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GuessGame");

            migrationBuilder.AddColumn<long>(
                name: "DiscordId",
                table: "GuessGameAttempt",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DiscordId",
                table: "GuessGame",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "DiscordId");

            migrationBuilder.CreateIndex(
                name: "IX_GuessGameAttempt_DiscordId",
                table: "GuessGameAttempt",
                column: "DiscordId");

            migrationBuilder.CreateIndex(
                name: "IX_GuessGame_DiscordId",
                table: "GuessGame",
                column: "DiscordId");

            migrationBuilder.AddForeignKey(
                name: "FK_GuessGame_User_DiscordId",
                table: "GuessGame",
                column: "DiscordId",
                principalTable: "User",
                principalColumn: "DiscordId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GuessGameAttempt_User_DiscordId",
                table: "GuessGameAttempt",
                column: "DiscordId",
                principalTable: "User",
                principalColumn: "DiscordId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuessGame_User_DiscordId",
                table: "GuessGame");

            migrationBuilder.DropForeignKey(
                name: "FK_GuessGameAttempt_User_DiscordId",
                table: "GuessGameAttempt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_GuessGameAttempt_DiscordId",
                table: "GuessGameAttempt");

            migrationBuilder.DropIndex(
                name: "IX_GuessGame_DiscordId",
                table: "GuessGame");

            migrationBuilder.DropColumn(
                name: "DiscordId",
                table: "GuessGameAttempt");

            migrationBuilder.DropColumn(
                name: "DiscordId",
                table: "GuessGame");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "User",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GuessGameAttempt",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GuessGame",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_DiscordId",
                table: "User",
                column: "DiscordId");

            migrationBuilder.CreateIndex(
                name: "IX_GuessGameAttempt_UserId",
                table: "GuessGameAttempt",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GuessGame_UserId",
                table: "GuessGame",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GuessGame_User_UserId",
                table: "GuessGame",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GuessGameAttempt_User_UserId",
                table: "GuessGameAttempt",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
