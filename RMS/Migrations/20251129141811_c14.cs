using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class c14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zerib",
                table: "tblItemFBStar");

            migrationBuilder.AddColumn<bool>(
                name: "blnKharidTajhizat",
                table: "tblItemFBStar",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "blnKharidTajhizat",
                table: "tblItemFBStar");

            migrationBuilder.AddColumn<decimal>(
                name: "Zerib",
                table: "tblItemFBStar",
                type: "decimal(18,4)",
                nullable: true);
        }
    }
}
