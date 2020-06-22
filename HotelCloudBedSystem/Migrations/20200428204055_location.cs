using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelCloudBedSystem.Migrations
{
    public partial class location : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CityMapLocation",
                columns: table => new
                {
                    CityMapLocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityName = table.Column<string>(nullable: true),
                    Latitude = table.Column<int>(nullable: false),
                    Longitude = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityMapLocation", x => x.CityMapLocationId);
                });

            migrationBuilder.CreateTable(
                name: "HotelMapLocation",
                columns: table => new
                {
                    HotelMapLocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HotelName = table.Column<string>(nullable: true),
                    Latitude = table.Column<int>(nullable: false),
                    Longitude = table.Column<int>(nullable: false),
                    CityMapLocationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelMapLocation", x => x.HotelMapLocationId);
                    table.ForeignKey(
                        name: "FK_HotelMapLocation_CityMapLocation_CityMapLocationId",
                        column: x => x.CityMapLocationId,
                        principalTable: "CityMapLocation",
                        principalColumn: "CityMapLocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelMapLocation_CityMapLocationId",
                table: "HotelMapLocation",
                column: "CityMapLocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelMapLocation");

            migrationBuilder.DropTable(
                name: "CityMapLocation");
        }
    }
}
