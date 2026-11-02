using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class h19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinimumRequiredResponses",
                table: "tblWorkflowStep",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceiverMode",
                table: "tblWorkflowStep",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrentStepId",
                table: "tblWorkflowInstance",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "tblWorkflowTransition",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkflowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<int>(type: "int", nullable: false),
                    IsEnd = table.Column<bool>(type: "bit", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
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
                name: "IX_tblWorkflowInstance_CurrentStepId",
                table: "tblWorkflowInstance",
                column: "CurrentStepId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_tblWorkflowInstance_tblWorkflowStep_CurrentStepId",
                table: "tblWorkflowInstance",
                column: "CurrentStepId",
                principalTable: "tblWorkflowStep",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblWorkflowInstance_tblWorkflowStep_CurrentStepId",
                table: "tblWorkflowInstance");

            migrationBuilder.DropTable(
                name: "tblWorkflowTransition");

            migrationBuilder.DropIndex(
                name: "IX_tblWorkflowInstance_CurrentStepId",
                table: "tblWorkflowInstance");

            migrationBuilder.DropColumn(
                name: "MinimumRequiredResponses",
                table: "tblWorkflowStep");

            migrationBuilder.DropColumn(
                name: "ReceiverMode",
                table: "tblWorkflowStep");

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrentStepId",
                table: "tblWorkflowInstance",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
