using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeatingAPI.Migrations
{
    /// <inheritdoc />
    public partial class joins_andbookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Locations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Locations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Locations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Locations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "BookingRequest",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "BookingRequest",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "BookingRequest",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "BookingRequest",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "BookingRequest");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "BookingRequest");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "BookingRequest");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "BookingRequest");
        }
    }
}
