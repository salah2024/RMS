using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class h22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkflowActionViewType",
                table: "tblWorkflowStep");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "tblWorkflowAction");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "tblWorkflowAction");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkflowActionViewType",
                table: "tblWorkflowStep",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "tblWorkflowAction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "tblWorkflowAction",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
