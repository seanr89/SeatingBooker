using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeatingAPI.Migrations
{
    /// <inheritdoc />
    public partial class deskupdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SeatMap",
                table: "Locations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeatType",
                table: "Desks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeatMap",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "SeatType",
                table: "Desks");
        }
    }
}
