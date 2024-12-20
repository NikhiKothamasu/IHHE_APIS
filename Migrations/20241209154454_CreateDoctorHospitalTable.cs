using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Apis.Migrations
{
    /// <inheritdoc />
    public partial class CreateDoctorHospitalTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "doctor_Registrations",
                columns: table => new
                {
                    Doctor_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Doctor_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_Field_Study = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_Hospital = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_User_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctor_Registrations", x => x.Doctor_Id);
                });

            migrationBuilder.CreateTable(
                name: "hospital_registrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Hospital_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_User_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_Phone_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_Avilable_Facilities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_Account_Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_Account__Confirm_password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_Established_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Hospital_Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_Ownership_Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hospital_registrations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "doctor_Registrations");

            migrationBuilder.DropTable(
                name: "hospital_registrations");
        }
    }
}
