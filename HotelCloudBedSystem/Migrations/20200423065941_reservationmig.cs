using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelCloudBedSystem.Migrations
{
    public partial class reservationmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomReservation_AspNetUsers_AppUserId",
                table: "RoomReservation");

            migrationBuilder.DropIndex(
                name: "IX_RoomReservation_AppUserId",
                table: "RoomReservation");

            migrationBuilder.RenameColumn(
                name: "Chk_out_date",
                table: "RoomReservation",
                newName: "ChkOutdate");

            migrationBuilder.RenameColumn(
                name: "Chk_in_date",
                table: "RoomReservation",
                newName: "ChkIndate");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "RoomReservation",
                newName: "UserName");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "RoomReservation",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "RoomReservation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cnic",
                table: "RoomReservation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNo",
                table: "RoomReservation",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomNo",
                table: "RoomReservation",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "RoomReservation");

            migrationBuilder.DropColumn(
                name: "Cnic",
                table: "RoomReservation");

            migrationBuilder.DropColumn(
                name: "PhoneNo",
                table: "RoomReservation");

            migrationBuilder.DropColumn(
                name: "RoomNo",
                table: "RoomReservation");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "RoomReservation",
                newName: "AppUserId");

            migrationBuilder.RenameColumn(
                name: "ChkOutdate",
                table: "RoomReservation",
                newName: "Chk_out_date");

            migrationBuilder.RenameColumn(
                name: "ChkIndate",
                table: "RoomReservation",
                newName: "Chk_in_date");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "RoomReservation",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomReservation_AppUserId",
                table: "RoomReservation",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomReservation_AspNetUsers_AppUserId",
                table: "RoomReservation",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
