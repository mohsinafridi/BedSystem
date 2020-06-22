 using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelCloudBedSystem.Migrations
{
    public partial class hotelmigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "HotelFacilities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotelFacilities_HotelId",
                table: "HotelFacilities",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelFacilities_Hotel_HotelId",
                table: "HotelFacilities",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelFacilities_Hotel_HotelId",
                table: "HotelFacilities");

            migrationBuilder.DropIndex(
                name: "IX_HotelFacilities_HotelId",
                table: "HotelFacilities");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "HotelFacilities");
        }
    }
}
