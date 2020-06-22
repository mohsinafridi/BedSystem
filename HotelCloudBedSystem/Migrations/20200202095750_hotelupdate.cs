using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelCloudBedSystem.Migrations
{
    public partial class hotelupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutHotel",
                table: "Hotel",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZipCode",
                table: "Hotel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "RequestAccept",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutHotel",
                table: "Hotel");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Hotel");

            migrationBuilder.DropColumn(
                name: "RequestAccept",
                table: "AspNetUsers");
        }
    }
}
