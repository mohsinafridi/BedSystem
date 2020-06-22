using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelCloudBedSystem.Migrations
{
    public partial class hotelmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    HotelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HotelName = table.Column<string>(nullable: true),
                    NoOfFloors = table.Column<int>(nullable: false),
                    NoOfRooms = table.Column<int>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.HotelId);
                    table.ForeignKey(
                        name: "FK_Hotel_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HotelImages",
                columns: table => new
                {
                    HotelImagesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HImage = table.Column<byte[]>(nullable: true),
                    HotelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelImages", x => x.HotelImagesId);
                    table.ForeignKey(
                        name: "FK_HotelImages_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HotelRoom",
                columns: table => new
                {
                    HotelRoomId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoomType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 400, nullable: true),
                    HotelId = table.Column<int>(nullable: true),
                    OcupancyLimit = table.Column<int>(nullable: false),
                    IsBooked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelRoom", x => x.HotelRoomId);
                    table.ForeignKey(
                        name: "FK_HotelRoom_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomReservation",
                columns: table => new
                {
                    RoomReservationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Chk_in_date = table.Column<DateTime>(nullable: false),
                    Chk_out_date = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true),
                    HotelRoomId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomReservation", x => x.RoomReservationId);
                    table.ForeignKey(
                        name: "FK_RoomReservation_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoomReservation_HotelRoom_HotelRoomId",
                        column: x => x.HotelRoomId,
                        principalTable: "HotelRoom",
                        principalColumn: "HotelRoomId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomsImage",
                columns: table => new
                {
                    RoomsImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HotelRoomId = table.Column<int>(nullable: true),
                    images = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomsImage", x => x.RoomsImageId);
                    table.ForeignKey(
                        name: "FK_RoomsImage_HotelRoom_HotelRoomId",
                        column: x => x.HotelRoomId,
                        principalTable: "HotelRoom",
                        principalColumn: "HotelRoomId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PaymentType = table.Column<string>(nullable: true),
                    RoomReservationId = table.Column<int>(nullable: true),
                    HotelRoomId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payment_HotelRoom_HotelRoomId",
                        column: x => x.HotelRoomId,
                        principalTable: "HotelRoom",
                        principalColumn: "HotelRoomId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payment_RoomReservation_RoomReservationId",
                        column: x => x.RoomReservationId,
                        principalTable: "RoomReservation",
                        principalColumn: "RoomReservationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_AppUserId",
                table: "Hotel",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelImages_HotelId",
                table: "HotelImages",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelRoom_HotelId",
                table: "HotelRoom",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_HotelRoomId",
                table: "Payment",
                column: "HotelRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_RoomReservationId",
                table: "Payment",
                column: "RoomReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomReservation_AppUserId",
                table: "RoomReservation",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomReservation_HotelRoomId",
                table: "RoomReservation",
                column: "HotelRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomsImage_HotelRoomId",
                table: "RoomsImage",
                column: "HotelRoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelImages");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "RoomsImage");

            migrationBuilder.DropTable(
                name: "RoomReservation");

            migrationBuilder.DropTable(
                name: "HotelRoom");

            migrationBuilder.DropTable(
                name: "Hotel");
        }
    }
}
