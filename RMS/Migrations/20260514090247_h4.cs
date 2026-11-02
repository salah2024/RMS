using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class h4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoeFBId",
                table: "tblZaribPishnahadiPaymanKar");

            migrationBuilder.AddColumn<int>(
                name: "NoeFBId",
                table: "tblZaribPishnahadiPaymanKarFosoul",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoeFBId",
                table: "tblZaribPishnahadiPaymanKarFosoul");

            migrationBuilder.AddColumn<int>(
                name: "NoeFBId",
                table: "tblZaribPishnahadiPaymanKar",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
