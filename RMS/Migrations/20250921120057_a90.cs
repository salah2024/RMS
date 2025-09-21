using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a90 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromKM",
                table: "tblKhakRiziBarAvord",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "KMNum",
                table: "tblKhakRiziBarAvord",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ToKM",
                table: "tblKhakRiziBarAvord",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "tblKhakRiziBarAvord",
                type: "decimal(18,4)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromKM",
                table: "tblKhakRiziBarAvord");

            migrationBuilder.DropColumn(
                name: "KMNum",
                table: "tblKhakRiziBarAvord");

            migrationBuilder.DropColumn(
                name: "ToKM",
                table: "tblKhakRiziBarAvord");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "tblKhakRiziBarAvord");
        }
    }
}
