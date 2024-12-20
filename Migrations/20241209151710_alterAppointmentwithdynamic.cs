using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Apis.Migrations
{
    /// <inheritdoc />
    public partial class alterAppointmentwithdynamic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Speciality",
                table: "Appointments",
                newName: "Doctor");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Appointments",
                newName: "AppointmentNote");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Doctor",
                table: "Appointments",
                newName: "Speciality");

            migrationBuilder.RenameColumn(
                name: "AppointmentNote",
                table: "Appointments",
                newName: "Location");
        }
    }
}
