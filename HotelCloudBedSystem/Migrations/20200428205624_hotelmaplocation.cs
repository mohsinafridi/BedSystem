using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelCloudBedSystem.Migrations
{
    public partial class hotelmaplocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "HotelMapLocation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "HotelMapLocation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "HotelMapLocation");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "HotelMapLocation");
        }
    }
}
