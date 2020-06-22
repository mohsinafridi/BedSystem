using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelCloudBedSystem.Migrations
{
    public partial class res_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomNo",
                table: "RoomReservation",
                newName: "ZipCode");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "RoomReservation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCity",
                table: "RoomReservation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "RoomReservation");

            migrationBuilder.DropColumn(
                name: "UserCity",
                table: "RoomReservation");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "RoomReservation",
                newName: "RoomNo");
        }
    }
}
