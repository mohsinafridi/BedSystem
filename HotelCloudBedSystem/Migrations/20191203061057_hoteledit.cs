using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelCloudBedSystem.Migrations
{
    public partial class hoteledit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Hotel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HotelCity",
                table: "Hotel",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Hotel");

            migrationBuilder.DropColumn(
                name: "HotelCity",
                table: "Hotel");
        }
    }
}
