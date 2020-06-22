using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelCloudBedSystem.Migrations
{
    public partial class addlong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "CityMapLocation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "longtitude",
                table: "CityMapLocation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "CityMapLocation");

            migrationBuilder.DropColumn(
                name: "longtitude",
                table: "CityMapLocation");
        }
    }
}
