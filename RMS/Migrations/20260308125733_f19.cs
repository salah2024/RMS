using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class f19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoeFB",
                table: "tblItemsHasConditionAddedToFB",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoeFB",
                table: "tblItemsHasCondition",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoeFB",
                table: "tblItemsHasConditionAddedToFB");

            migrationBuilder.DropColumn(
                name: "NoeFB",
                table: "tblItemsHasCondition");
        }
    }
}
