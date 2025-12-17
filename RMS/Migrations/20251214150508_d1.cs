using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class d1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZaribManteghe",
                table: "tblBaravordZaribManteghe");

            migrationBuilder.DropColumn(
                name: "ZaribBalasari",
                table: "tblBaravordZaribBalaSari");

            migrationBuilder.AlterColumn<decimal>(
                name: "Tarh",
                table: "tblBaravordZaribBalaSari",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ZaribManteghe",
                table: "tblBaravordZaribManteghe",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Tarh",
                table: "tblBaravordZaribBalaSari",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AddColumn<decimal>(
                name: "ZaribBalasari",
                table: "tblBaravordZaribBalaSari",
                type: "decimal(18,4)",
                nullable: true);
        }
    }
}
