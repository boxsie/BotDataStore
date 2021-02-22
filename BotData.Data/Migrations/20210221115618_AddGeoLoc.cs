using Microsoft.EntityFrameworkCore.Migrations;

namespace BotData.Data.Migrations
{
    public partial class AddGeoLoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeoSniffLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: true),
                    Area = table.Column<string>(type: "text", nullable: true),
                    SubArea = table.Column<string>(type: "text", nullable: true),
                    Lat = table.Column<double>(type: "double precision", nullable: false),
                    Long = table.Column<double>(type: "double precision", nullable: false),
                    Radius = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoSniffLocation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeoSniffLocation_Country",
                table: "GeoSniffLocation",
                column: "Country");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeoSniffLocation");
        }
    }
}
