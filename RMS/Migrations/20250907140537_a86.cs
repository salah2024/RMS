using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a86 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Arz",
                table: "tblAmalyateKhakiInfoForBarAvordEzafeBaha");

            migrationBuilder.DropColumn(
                name: "Tool",
                table: "tblAmalyateKhakiInfoForBarAvordEzafeBaha");

            migrationBuilder.AddColumn<string>(
                name: "Condition",
                table: "tblNoeKhakBardariEzafeBaha",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Condition",
                table: "tblNoeKhakBardariEzafeBaha");

            migrationBuilder.AddColumn<decimal>(
                name: "Arz",
                table: "tblAmalyateKhakiInfoForBarAvordEzafeBaha",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Tool",
                table: "tblAmalyateKhakiInfoForBarAvordEzafeBaha",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
