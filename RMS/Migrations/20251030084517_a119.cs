using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a119 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EssentialValue",
                table: "tblItemsFields",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EssentialValue",
                table: "tblItemsFields");
        }
    }
}
