using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelCloudBedSystem.Migrations
{
    public partial class roomid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HotelRoomId",
                table: "RoomFacilities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomFacilities_HotelRoomId",
                table: "RoomFacilities",
                column: "HotelRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFacilities_HotelRoom_HotelRoomId",
                table: "RoomFacilities",
                column: "HotelRoomId",
                principalTable: "HotelRoom",
                principalColumn: "HotelRoomId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomFacilities_HotelRoom_HotelRoomId",
                table: "RoomFacilities");

            migrationBuilder.DropIndex(
                name: "IX_RoomFacilities_HotelRoomId",
                table: "RoomFacilities");

            migrationBuilder.DropColumn(
                name: "HotelRoomId",
                table: "RoomFacilities");
        }
    }
}
