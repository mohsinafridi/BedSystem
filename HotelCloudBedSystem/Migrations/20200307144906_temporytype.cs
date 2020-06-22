using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelCloudBedSystem.Migrations
{
    public partial class temporytype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TemporaryRoomType",
                columns: table => new
                {
                    TemporaryRoomTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HotelRoomTypeId = table.Column<int>(nullable: true),
                    HotelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryRoomType", x => x.TemporaryRoomTypeId);
                    table.ForeignKey(
                        name: "FK_TemporaryRoomType_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporaryRoomType_HotelRoomType_HotelRoomTypeId",
                        column: x => x.HotelRoomTypeId,
                        principalTable: "HotelRoomType",
                        principalColumn: "HotelRoomTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryRoomType_HotelId",
                table: "TemporaryRoomType",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryRoomType_HotelRoomTypeId",
                table: "TemporaryRoomType",
                column: "HotelRoomTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemporaryRoomType");
        }
    }
}
