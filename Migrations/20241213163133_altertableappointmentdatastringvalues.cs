using Microsoft.EntityFrameworkCore.Migrations;


#nullable disable

namespace Project_Apis.Migrations
{
    /// <inheritdoc />
    public partial class altertableappointmentdatastringvalues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "blood_pressure",
                table: "AppointmentDatas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "blood_pressure",
                table: "AppointmentDatas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
