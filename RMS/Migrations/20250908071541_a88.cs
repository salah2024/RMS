using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a88 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EnableDeleting",
                table: "tblNoeKhakBardariEzafeBaha",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EnableEditing",
                table: "tblNoeKhakBardariEzafeBaha",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnableDeleting",
                table: "tblNoeKhakBardariEzafeBaha");

            migrationBuilder.DropColumn(
                name: "EnableEditing",
                table: "tblNoeKhakBardariEzafeBaha");
        }
    }
}
