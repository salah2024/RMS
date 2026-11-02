using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class j3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkFlowId",
                schema: "dbo",
                table: "tblBarAvordProjectionWorkFlow");

            migrationBuilder.AddColumn<int>(
                name: "proccesType",
                schema: "dbo",
                table: "tblBarAvordProjectionWorkFlow",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "proccesType",
                schema: "dbo",
                table: "tblBarAvordProjectionWorkFlow");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkFlowId",
                schema: "dbo",
                table: "tblBarAvordProjectionWorkFlow",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
