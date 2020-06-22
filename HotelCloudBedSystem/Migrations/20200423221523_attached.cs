using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelCloudBedSystem.Migrations
{
    public partial class attached : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WashRoom",
                table: "RoomFacilities",
                newName: "AttachedWashRoom");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttachedWashRoom",
                table: "RoomFacilities",
                newName: "WashRoom");
        }
    }
}
