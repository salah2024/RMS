using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class h17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "tblWorkflowInstance");

            migrationBuilder.DropColumn(
                name: "FinishDate",
                table: "tblWorkflowInstance");

            migrationBuilder.DropColumn(
                name: "ActionDate",
                table: "tblWorkflowAssignment");

            migrationBuilder.RenameColumn(
                name: "EntityName",
                table: "tblWorkflowInstance",
                newName: "EntityType");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "tblWorkflowStepUser",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "tblWorkflowStepRole",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "RequireForm",
                table: "tblWorkflowStep",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkflowActionId",
                table: "tblWorkflowStep",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "tblWorkflowAssignment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "tblWorkflow",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSystem",
                table: "tblWorkflow",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerUserId",
                table: "tblWorkflow",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "proccesType",
                table: "tblWorkflow",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BaravordUserAccesses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaravordUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessType = table.Column<int>(type: "int", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaravordUserAccesses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BaravordUserAccesses_tblBaravordUser_BaravordUserId",
                        column: x => x.BaravordUserId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblBaravordWorkflow",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaravordUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkflowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBaravordWorkflow", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblBaravordWorkflow_tblBaravordUser_BaravordUserId",
                        column: x => x.BaravordUserId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblBaravordWorkflow_tblWorkflow_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "tblWorkflow",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblWorkflowAction",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Controller = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWorkflowAction", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblWorkflowStepUser_UserId",
                table: "tblWorkflowStepUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWorkflowStepRole_RoleId",
                table: "tblWorkflowStepRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWorkflowStep_WorkflowActionId",
                table: "tblWorkflowStep",
                column: "WorkflowActionId");

            migrationBuilder.CreateIndex(
                name: "IX_BaravordUserAccesses_BaravordUserId",
                table: "BaravordUserAccesses",
                column: "BaravordUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBaravordWorkflow_BaravordUserId",
                table: "tblBaravordWorkflow",
                column: "BaravordUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBaravordWorkflow_WorkflowId",
                table: "tblBaravordWorkflow",
                column: "WorkflowId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblWorkflowStep_tblWorkflowAction_WorkflowActionId",
                table: "tblWorkflowStep",
                column: "WorkflowActionId",
                principalTable: "tblWorkflowAction",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblWorkflowStepRole_AspNetRoles_RoleId",
                table: "tblWorkflowStepRole",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblWorkflowStepUser_AspNetUsers_UserId",
                table: "tblWorkflowStepUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblWorkflowStep_tblWorkflowAction_WorkflowActionId",
                table: "tblWorkflowStep");

            migrationBuilder.DropForeignKey(
                name: "FK_tblWorkflowStepRole_AspNetRoles_RoleId",
                table: "tblWorkflowStepRole");

            migrationBuilder.DropForeignKey(
                name: "FK_tblWorkflowStepUser_AspNetUsers_UserId",
                table: "tblWorkflowStepUser");

            migrationBuilder.DropTable(
                name: "BaravordUserAccesses");

            migrationBuilder.DropTable(
                name: "tblBaravordWorkflow");

            migrationBuilder.DropTable(
                name: "tblWorkflowAction");

            migrationBuilder.DropIndex(
                name: "IX_tblWorkflowStepUser_UserId",
                table: "tblWorkflowStepUser");

            migrationBuilder.DropIndex(
                name: "IX_tblWorkflowStepRole_RoleId",
                table: "tblWorkflowStepRole");

            migrationBuilder.DropIndex(
                name: "IX_tblWorkflowStep_WorkflowActionId",
                table: "tblWorkflowStep");

            migrationBuilder.DropColumn(
                name: "RequireForm",
                table: "tblWorkflowStep");

            migrationBuilder.DropColumn(
                name: "WorkflowActionId",
                table: "tblWorkflowStep");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "tblWorkflowAssignment");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "tblWorkflow");

            migrationBuilder.DropColumn(
                name: "IsSystem",
                table: "tblWorkflow");

            migrationBuilder.DropColumn(
                name: "OwnerUserId",
                table: "tblWorkflow");

            migrationBuilder.DropColumn(
                name: "proccesType",
                table: "tblWorkflow");

            migrationBuilder.RenameColumn(
                name: "EntityType",
                table: "tblWorkflowInstance",
                newName: "EntityName");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "tblWorkflowStepUser",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "tblWorkflowStepRole",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "tblWorkflowInstance",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishDate",
                table: "tblWorkflowInstance",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActionDate",
                table: "tblWorkflowAssignment",
                type: "datetime2",
                nullable: true);
        }
    }
}
