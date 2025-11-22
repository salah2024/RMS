using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class c8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Shahr",
                table: "tblBaravordZaribManteghe",
                newName: "ShahrId");

            migrationBuilder.RenameColumn(
                name: "Ostan",
                table: "tblBaravordZaribManteghe",
                newName: "OstanId");

            migrationBuilder.RenameColumn(
                name: "Bakhsh",
                table: "tblBaravordZaribManteghe",
                newName: "BakhshId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShahrId",
                table: "tblBaravordZaribManteghe",
                newName: "Shahr");

            migrationBuilder.RenameColumn(
                name: "OstanId",
                table: "tblBaravordZaribManteghe",
                newName: "Ostan");

            migrationBuilder.RenameColumn(
                name: "BakhshId",
                table: "tblBaravordZaribManteghe",
                newName: "Bakhsh");
        }
    }
}
