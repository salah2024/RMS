using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a41 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "tblConditionGroup",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConditionContextRel",
                table: "tblConditionContext",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "tblConditionGroup");

            migrationBuilder.DropColumn(
                name: "ConditionContextRel",
                table: "tblConditionContext");
        }
    }
}
