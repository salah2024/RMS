using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a61 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sharh",
                table: "tblBarAvordAddedBoard",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Tedad",
                table: "tblBarAvordAddedBoard",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sharh",
                table: "tblBarAvordAddedBoard");

            migrationBuilder.DropColumn(
                name: "Tedad",
                table: "tblBarAvordAddedBoard");
        }
    }
}
