using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelCloudBedSystem.Migrations
{
    public partial class roomreview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomReview",
                columns: table => new
                {
                    RoomReviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReviewStar = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Review = table.Column<string>(nullable: true),
                    HotelRoomId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomReview", x => x.RoomReviewId);
                    table.ForeignKey(
                        name: "FK_RoomReview_HotelRoom_HotelRoomId",
                        column: x => x.HotelRoomId,
                        principalTable: "HotelRoom",
                        principalColumn: "HotelRoomId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomReview_HotelRoomId",
                table: "RoomReview",
                column: "HotelRoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomReview");
        }
    }
}
