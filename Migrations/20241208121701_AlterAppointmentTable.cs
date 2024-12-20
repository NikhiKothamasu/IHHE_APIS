using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Apis.Migrations
{
    /// <inheritdoc />
    public partial class AlterAppointmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "time",
                table: "Appointments",
                newName: "AppointmentTime");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Appointments",
                newName: "AppointmentDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Appointments",
                newName: "AppointmentId");

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDateTime",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Speciality",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestDateTime",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Speciality",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "AppointmentTime",
                table: "Appointments",
                newName: "time");

            migrationBuilder.RenameColumn(
                name: "AppointmentDate",
                table: "Appointments",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "AppointmentId",
                table: "Appointments",
                newName: "Id");
        }
    }
}
