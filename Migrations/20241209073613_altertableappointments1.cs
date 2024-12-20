using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Apis.Migrations
{
    /// <inheritdoc />
    public partial class altertableappointments1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Doctor",
                table: "Appointments",
                newName: "Speciality");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "Speciality",
                table: "Appointments",
                newName: "Doctor");
        }
    }
}
