using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Apis.Migrations
{
    /// <inheritdoc />
    public partial class connecttabledoctorshospitals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "doctor_Registrations");

            migrationBuilder.DropTable(
                name: "hospital_registrations");

            migrationBuilder.CreateTable(
                name: "Hospitals",
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
                    Hospital_Ownership_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Account_Staus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Doctor_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Doctor_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_Field_Study = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HospitalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Doctor_Hospital = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Account_Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Doctor_Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_HospitalId",
                table: "Doctors",
                column: "HospitalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.CreateTable(
                name: "doctor_Registrations",
                columns: table => new
                {
                    Doctor_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Account_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_Field_Study = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_Hospital = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Account_Staus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hospital_Account_Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_Account__Confirm_password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_Avilable_Facilities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_Established_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Hospital_Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_Ownership_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_Phone_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hospital_User_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hospital_registrations", x => x.Id);
                });
        }
    }
}
