using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class h3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "tblZaribPishnahadiPaymanKar",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblZaribPishnahadiPaymanKar_CompanyId",
                table: "tblZaribPishnahadiPaymanKar",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblZaribPishnahadiPaymanKar_tblCompany_CompanyId",
                table: "tblZaribPishnahadiPaymanKar",
                column: "CompanyId",
                principalTable: "tblCompany",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblZaribPishnahadiPaymanKar_tblCompany_CompanyId",
                table: "tblZaribPishnahadiPaymanKar");

            migrationBuilder.DropIndex(
                name: "IX_tblZaribPishnahadiPaymanKar_CompanyId",
                table: "tblZaribPishnahadiPaymanKar");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "tblZaribPishnahadiPaymanKar");
        }
    }
}
