using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class f21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoeFB",
                table: "tblItemsHasCondition_ConditionContext",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoeFB",
                table: "tblItemsAddingToFB",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoeFB",
                table: "tblItemsHasCondition_ConditionContext");

            migrationBuilder.DropColumn(
                name: "NoeFB",
                table: "tblItemsAddingToFB");
        }
    }
}
