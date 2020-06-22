using Microsoft.EntityFrameworkCore.Migrations;

using System;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HotelCloudBedSystem.Migrations
{
    public partial class facilities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RoomImage",
                table: "HotelRoom",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HotelFacilities",
                columns: table => new
                {
                    HotelFacilitiesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FreeWifi = table.Column<bool>(nullable: false),
                    BreckFast = table.Column<bool>(nullable: false),
                    Lunch = table.Column<bool>(nullable: false),
                    AttachedWashrooms = table.Column<bool>(nullable: false),
                    Receptionservices = table.Column<bool>(nullable: false),
                    Dinner = table.Column<bool>(nullable: false),
                    CarParking = table.Column<bool>(nullable: false),
                    Laundry = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelFacilities", x => x.HotelFacilitiesId);
                });

            migrationBuilder.CreateTable(
                name: "RoomFacilities",
                columns: table => new
                {
                    RoomFacilitiesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Internet = table.Column<bool>(nullable: false),
                    Tv = table.Column<bool>(nullable: false),
                    WashRoom = table.Column<bool>(nullable: false),
                    Ac = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomFacilities", x => x.RoomFacilitiesId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelFacilities");

            migrationBuilder.DropTable(
                name: "RoomFacilities");

            migrationBuilder.DropColumn(
                name: "RoomImage",
                table: "HotelRoom");
        }
    }
}
