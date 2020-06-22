using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelCloudBedSystem.Migrations
{
    public partial class editroom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "HotelRoom",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotelRoom_HotelId",
                table: "HotelRoom",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRoom_Hotel_HotelId",
                table: "HotelRoom",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelRoom_Hotel_HotelId",
                table: "HotelRoom");

            migrationBuilder.DropIndex(
                name: "IX_HotelRoom_HotelId",
                table: "HotelRoom");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "HotelRoom");
        }
    }
}
