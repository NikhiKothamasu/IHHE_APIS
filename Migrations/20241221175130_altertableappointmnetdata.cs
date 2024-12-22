using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Apis.Migrations
{
    /// <inheritdoc />
    public partial class altertableappointmnetdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "height",
                table: "AppointmentDatas");

            migrationBuilder.AddColumn<string>(
                name: "prescritionnote",
                table: "AppointmentDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "prescritionnote",
                table: "AppointmentDatas");

            migrationBuilder.AddColumn<int>(
                name: "height",
                table: "AppointmentDatas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
