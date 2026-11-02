using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkflowAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CurrentStepId",
                table: "tblWorkflowInstance",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "AssignedByUserId",
                table: "tblWorkflowAssignment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "tblWorkflowAssignment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedDate",
                table: "tblWorkflowAssignment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Result",
                table: "tblWorkflowAssignment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkflowStepId",
                table: "tblWorkflowAssignment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tblWorkflowAssignment_WorkflowStepId",
                table: "tblWorkflowAssignment",
                column: "WorkflowStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblWorkflowAssignment_tblWorkflowStep_WorkflowStepId",
                table: "tblWorkflowAssignment",
                column: "WorkflowStepId",
                principalTable: "tblWorkflowStep",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblWorkflowAssignment_tblWorkflowStep_WorkflowStepId",
                table: "tblWorkflowAssignment");

            migrationBuilder.DropIndex(
                name: "IX_tblWorkflowAssignment_WorkflowStepId",
                table: "tblWorkflowAssignment");

            migrationBuilder.DropColumn(
                name: "AssignedByUserId",
                table: "tblWorkflowAssignment");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "tblWorkflowAssignment");

            migrationBuilder.DropColumn(
                name: "CompletedDate",
                table: "tblWorkflowAssignment");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "tblWorkflowAssignment");

            migrationBuilder.DropColumn(
                name: "WorkflowStepId",
                table: "tblWorkflowAssignment");

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrentStepId",
                table: "tblWorkflowInstance",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
