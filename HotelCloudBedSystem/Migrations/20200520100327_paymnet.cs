using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelCloudBedSystem.Migrations
{
    public partial class paymnet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "RoomReservation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaymentSuccessfull",
                table: "RoomReservation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NoOfNight",
                table: "RoomReservation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Payment",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "RoomReservation");

            migrationBuilder.DropColumn(
                name: "IsPaymentSuccessfull",
                table: "RoomReservation");

            migrationBuilder.DropColumn(
                name: "NoOfNight",
                table: "RoomReservation");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Payment");
        }
    }
}
