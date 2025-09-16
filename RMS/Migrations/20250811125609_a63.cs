using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a63 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PercentPrintPOP",
                table: "tblBarAvordAddedBoard",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UsePOP",
                table: "tblBarAvordAddedBoard",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PercentPrintPOP",
                table: "tblBarAvordAddedBoard");

            migrationBuilder.DropColumn(
                name: "UsePOP",
                table: "tblBarAvordAddedBoard");
        }
    }
}
