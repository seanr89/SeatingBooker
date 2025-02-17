using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeatingAPI.Migrations
{
    /// <inheritdoc />
    public partial class BookingStaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "BookingRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Desks_StaffId",
                table: "Desks",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingRequests_StaffId",
                table: "BookingRequests",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingRequests_Staff_StaffId",
                table: "BookingRequests",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Desks_Staff_StaffId",
                table: "Desks",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingRequests_Staff_StaffId",
                table: "BookingRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Desks_Staff_StaffId",
                table: "Desks");

            migrationBuilder.DropIndex(
                name: "IX_Desks_StaffId",
                table: "Desks");

            migrationBuilder.DropIndex(
                name: "IX_BookingRequests_StaffId",
                table: "BookingRequests");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "BookingRequests");
        }
    }
}
