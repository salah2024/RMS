using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class h20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblWorkflowTransition");

            migrationBuilder.AddColumn<Guid>(
                name: "NextStepId",
                table: "tblWorkflowStep",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PreviousStepId",
                table: "tblWorkflowStep",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoundNo",
                table: "tblWorkflowHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextStepId",
                table: "tblWorkflowStep");

            migrationBuilder.DropColumn(
                name: "PreviousStepId",
                table: "tblWorkflowStep");

            migrationBuilder.DropColumn(
                name: "RoundNo",
                table: "tblWorkflowHistory");

            migrationBuilder.CreateTable(
                name: "tblWorkflowTransition",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkflowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<int>(type: "int", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IsEnd = table.Column<bool>(type: "bit", nullable: false),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWorkflowTransition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblWorkflowTransition_tblWorkflowStep_FromStepId",
                        column: x => x.FromStepId,
                        principalTable: "tblWorkflowStep",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblWorkflowTransition_tblWorkflowStep_ToStepId",
                        column: x => x.ToStepId,
                        principalTable: "tblWorkflowStep",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblWorkflowTransition_tblWorkflow_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "tblWorkflow",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblWorkflowTransition_FromStepId",
                table: "tblWorkflowTransition",
                column: "FromStepId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWorkflowTransition_ToStepId",
                table: "tblWorkflowTransition",
                column: "ToStepId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWorkflowTransition_WorkflowId",
                table: "tblWorkflowTransition",
                column: "WorkflowId");
        }
    }
}
