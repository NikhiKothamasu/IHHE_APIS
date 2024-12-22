using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Apis.Migrations
{
    /// <inheritdoc />
    public partial class alterhospitalstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hospital_Account_Password",
                table: "Hospitals");

            migrationBuilder.RenameColumn(
                name: "Hospital_Type",
                table: "Hospitals",
                newName: "HospitalType");

            migrationBuilder.RenameColumn(
                name: "Hospital_Phone_number",
                table: "Hospitals",
                newName: "HospitalRegion");

            migrationBuilder.RenameColumn(
                name: "Hospital_Ownership_Type",
                table: "Hospitals",
                newName: "HospitalPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Hospital_Name",
                table: "Hospitals",
                newName: "HospitalOwnershipType");

            migrationBuilder.RenameColumn(
                name: "Hospital_Logo",
                table: "Hospitals",
                newName: "HospitalName");

            migrationBuilder.RenameColumn(
                name: "Hospital_Established_Date",
                table: "Hospitals",
                newName: "HospitalEstablishedDate");

            migrationBuilder.RenameColumn(
                name: "Hospital_Email",
                table: "Hospitals",
                newName: "HospitalEmail");

            migrationBuilder.RenameColumn(
                name: "Hospital_Code",
                table: "Hospitals",
                newName: "HospitalAddress");

            migrationBuilder.RenameColumn(
                name: "Hospital_Avilable_Facilities",
                table: "Hospitals",
                newName: "HospitalAccountPassword");

            migrationBuilder.RenameColumn(
                name: "Hospital_Address",
                table: "Hospitals",
                newName: "FounderName");

            migrationBuilder.RenameColumn(
                name: "Hospital_Account__Confirm_password",
                table: "Hospitals",
                newName: "AvailableFacilities");

            migrationBuilder.RenameColumn(
                name: "Account_Staus",
                table: "Hospitals",
                newName: "AccountStatus");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Hospitals",
                newName: "HospitalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HospitalType",
                table: "Hospitals",
                newName: "Hospital_Type");

            migrationBuilder.RenameColumn(
                name: "HospitalRegion",
                table: "Hospitals",
                newName: "Hospital_Phone_number");

            migrationBuilder.RenameColumn(
                name: "HospitalPhoneNumber",
                table: "Hospitals",
                newName: "Hospital_Ownership_Type");

            migrationBuilder.RenameColumn(
                name: "HospitalOwnershipType",
                table: "Hospitals",
                newName: "Hospital_Name");

            migrationBuilder.RenameColumn(
                name: "HospitalName",
                table: "Hospitals",
                newName: "Hospital_Logo");

            migrationBuilder.RenameColumn(
                name: "HospitalEstablishedDate",
                table: "Hospitals",
                newName: "Hospital_Established_Date");

            migrationBuilder.RenameColumn(
                name: "HospitalEmail",
                table: "Hospitals",
                newName: "Hospital_Email");

            migrationBuilder.RenameColumn(
                name: "HospitalAddress",
                table: "Hospitals",
                newName: "Hospital_Code");

            migrationBuilder.RenameColumn(
                name: "HospitalAccountPassword",
                table: "Hospitals",
                newName: "Hospital_Avilable_Facilities");

            migrationBuilder.RenameColumn(
                name: "FounderName",
                table: "Hospitals",
                newName: "Hospital_Address");

            migrationBuilder.RenameColumn(
                name: "AvailableFacilities",
                table: "Hospitals",
                newName: "Hospital_Account__Confirm_password");

            migrationBuilder.RenameColumn(
                name: "AccountStatus",
                table: "Hospitals",
                newName: "Account_Staus");

            migrationBuilder.RenameColumn(
                name: "HospitalId",
                table: "Hospitals",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Hospital_Account_Password",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
