using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelCloudBedSystem.Migrations
{
    public partial class _hotelfloors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelRoom_Hotel_HotelId",
                table: "HotelRoom");

            migrationBuilder.RenameColumn(
                name: "HotelId",
                table: "HotelRoom",
                newName: "HotelFloorsId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelRoom_HotelId",
                table: "HotelRoom",
                newName: "IX_HotelRoom_HotelFloorsId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "HotelRoom",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 400,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "HotelFloors",
                columns: table => new
                {
                    HotelFloorsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FloorNo = table.Column<int>(nullable: false),
                    HotelId = table.Column<int>(nullable: true),
                    NoOfRooms = table.Column<int>(nullable: false),
                    FloorName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelFloors", x => x.HotelFloorsId);
                    table.ForeignKey(
                        name: "FK_HotelFloors_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelFloors_HotelId",
                table: "HotelFloors",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRoom_HotelFloors_HotelFloorsId",
                table: "HotelRoom",
                column: "HotelFloorsId",
                principalTable: "HotelFloors",
                principalColumn: "HotelFloorsId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelRoom_HotelFloors_HotelFloorsId",
                table: "HotelRoom");

            migrationBuilder.DropTable(
                name: "HotelFloors");

            migrationBuilder.RenameColumn(
                name: "HotelFloorsId",
                table: "HotelRoom",
                newName: "HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelRoom_HotelFloorsId",
                table: "HotelRoom",
                newName: "IX_HotelRoom_HotelId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "HotelRoom",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRoom_Hotel_HotelId",
                table: "HotelRoom",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
