using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class b4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Zarib",
                table: "tblItemsRelatedToItemHaml",
                newName: "Zarib3");

            migrationBuilder.AddColumn<decimal>(
                name: "Zarib1",
                table: "tblItemsRelatedToItemHaml",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Zarib2",
                table: "tblItemsRelatedToItemHaml",
                type: "decimal(18,4)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zarib1",
                table: "tblItemsRelatedToItemHaml");

            migrationBuilder.DropColumn(
                name: "Zarib2",
                table: "tblItemsRelatedToItemHaml");

            migrationBuilder.RenameColumn(
                name: "Zarib3",
                table: "tblItemsRelatedToItemHaml",
                newName: "Zarib");
        }
    }
}
