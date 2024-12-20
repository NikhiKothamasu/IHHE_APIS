using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Apis.Migrations
{
    /// <inheritdoc />
    public partial class renamedoctorcols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hospital_User_Name",
                table: "Hospitals");

            migrationBuilder.RenameColumn(
                name: "Doctor_Password",
                table: "Doctors",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Doctor_Name",
                table: "Doctors",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Doctor_Mobile",
                table: "Doctors",
                newName: "Mobile");

            migrationBuilder.RenameColumn(
                name: "Doctor_Hospital",
                table: "Doctors",
                newName: "FieldOfStudy");

            migrationBuilder.RenameColumn(
                name: "Doctor_Field_Study",
                table: "Doctors",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Doctor_Email",
                table: "Doctors",
                newName: "AssociatedHospital");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Doctors",
                newName: "Doctor_Password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Doctors",
                newName: "Doctor_Name");

            migrationBuilder.RenameColumn(
                name: "Mobile",
                table: "Doctors",
                newName: "Doctor_Mobile");

            migrationBuilder.RenameColumn(
                name: "FieldOfStudy",
                table: "Doctors",
                newName: "Doctor_Hospital");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Doctors",
                newName: "Doctor_Field_Study");

            migrationBuilder.RenameColumn(
                name: "AssociatedHospital",
                table: "Doctors",
                newName: "Doctor_Email");

            migrationBuilder.AddColumn<string>(
                name: "Hospital_User_Name",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
