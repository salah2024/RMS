using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class f13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblUserDetail_tblCompany_CompanyId",
                table: "tblUserDetail");

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                table: "tblUserDetail",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "tblUserDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "tblUserDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_tblUserDetail_tblCompany_CompanyId",
                table: "tblUserDetail",
                column: "CompanyId",
                principalTable: "tblCompany",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblUserDetail_tblCompany_CompanyId",
                table: "tblUserDetail");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "tblUserDetail");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "tblUserDetail");

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                table: "tblUserDetail",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tblUserDetail_tblCompany_CompanyId",
                table: "tblUserDetail",
                column: "CompanyId",
                principalTable: "tblCompany",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
