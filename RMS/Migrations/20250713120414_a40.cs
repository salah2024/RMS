using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a40 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaxValue",
                table: "tblItemsHasCondition_ConditionContext",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MinValue",
                table: "tblItemsHasCondition_ConditionContext",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxValue",
                table: "tblItemsHasCondition_ConditionContext");

            migrationBuilder.DropColumn(
                name: "MinValue",
                table: "tblItemsHasCondition_ConditionContext");
        }
    }
}
