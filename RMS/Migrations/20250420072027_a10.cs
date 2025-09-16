using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDateTime",
                table: "tblItemsHasConditionAddedToFB",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "tblItemsHasConditionAddedToFB",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveDateTime",
                table: "tblItemsHasConditionAddedToFB",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserInserter",
                table: "tblItemsHasConditionAddedToFB",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserRemover",
                table: "tblItemsHasConditionAddedToFB",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuesForAbnieFaniValuesDto",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionForAbnieFaniId = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ShomarehFBSelectedId = table.Column<int>(type: "int", nullable: false),
                    PolVaAbroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Shomareh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuesForAbnieFaniValuesDto");

            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "tblItemsHasConditionAddedToFB");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "tblItemsHasConditionAddedToFB");

            migrationBuilder.DropColumn(
                name: "RemoveDateTime",
                table: "tblItemsHasConditionAddedToFB");

            migrationBuilder.DropColumn(
                name: "UserInserter",
                table: "tblItemsHasConditionAddedToFB");

            migrationBuilder.DropColumn(
                name: "UserRemover",
                table: "tblItemsHasConditionAddedToFB");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "tblItemsHasConditionAddedToFB",
                newName: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "tblItemsHasConditionAddedToFB",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
