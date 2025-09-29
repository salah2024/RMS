using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a98 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "hasDelButton",
                table: "tblEzafeBahaKhakRizi",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "hasEditButton",
                table: "tblEzafeBahaKhakRizi",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hasDelButton",
                table: "tblEzafeBahaKhakRizi");

            migrationBuilder.DropColumn(
                name: "hasEditButton",
                table: "tblEzafeBahaKhakRizi");
        }
    }
}
