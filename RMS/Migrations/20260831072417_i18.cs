using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class i18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BahayeVahedZarib",
                table: "tblFB");

            migrationBuilder.AddColumn<decimal>(
                name: "Zarib",
                table: "tblRizMetreUsers",
                type: "decimal(18,4)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zarib",
                table: "tblRizMetreUsers");

            migrationBuilder.AddColumn<decimal>(
                name: "BahayeVahedZarib",
                table: "tblFB",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
