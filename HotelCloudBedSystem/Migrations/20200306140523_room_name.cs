using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelCloudBedSystem.Migrations
{
    public partial class room_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoomName",
                table: "HotelRoom",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomNo",
                table: "HotelRoom",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomName",
                table: "HotelRoom");

            migrationBuilder.DropColumn(
                name: "RoomNo",
                table: "HotelRoom");
        }
    }
}
