using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelCloudBedSystem.Migrations
{
    public partial class roomtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomType",
                table: "HotelRoom");

            migrationBuilder.AddColumn<int>(
                name: "HotelRoomTypeId",
                table: "HotelRoom",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HotelRoomType",
                columns: table => new
                {
                    HotelRoomTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoomType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelRoomType", x => x.HotelRoomTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelRoom_HotelRoomTypeId",
                table: "HotelRoom",
                column: "HotelRoomTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRoom_HotelRoomType_HotelRoomTypeId",
                table: "HotelRoom",
                column: "HotelRoomTypeId",
                principalTable: "HotelRoomType",
                principalColumn: "HotelRoomTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelRoom_HotelRoomType_HotelRoomTypeId",
                table: "HotelRoom");

            migrationBuilder.DropTable(
                name: "HotelRoomType");

            migrationBuilder.DropIndex(
                name: "IX_HotelRoom_HotelRoomTypeId",
                table: "HotelRoom");

            migrationBuilder.DropColumn(
                name: "HotelRoomTypeId",
                table: "HotelRoom");

            migrationBuilder.AddColumn<int>(
                name: "RoomType",
                table: "HotelRoom",
                nullable: false,
                defaultValue: 0);
        }
    }
}
