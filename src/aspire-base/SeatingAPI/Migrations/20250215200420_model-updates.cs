using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeatingAPI.Migrations
{
    /// <inheritdoc />
    public partial class modelupdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingRequest_Desks_DeskId",
                table: "BookingRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingRequest",
                table: "BookingRequest");

            migrationBuilder.RenameTable(
                name: "BookingRequest",
                newName: "BookingRequests");

            migrationBuilder.RenameIndex(
                name: "IX_BookingRequest_DeskId",
                table: "BookingRequests",
                newName: "IX_BookingRequests_DeskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingRequests",
                table: "BookingRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingRequests_Desks_DeskId",
                table: "BookingRequests",
                column: "DeskId",
                principalTable: "Desks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingRequests_Desks_DeskId",
                table: "BookingRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingRequests",
                table: "BookingRequests");

            migrationBuilder.RenameTable(
                name: "BookingRequests",
                newName: "BookingRequest");

            migrationBuilder.RenameIndex(
                name: "IX_BookingRequests_DeskId",
                table: "BookingRequest",
                newName: "IX_BookingRequest_DeskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingRequest",
                table: "BookingRequest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingRequest_Desks_DeskId",
                table: "BookingRequest",
                column: "DeskId",
                principalTable: "Desks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
