using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class g5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "tblBaravordUser",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblBaravordUser_ParentId",
                table: "tblBaravordUser",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblBaravordUser_tblBaravordUser_ParentId",
                table: "tblBaravordUser",
                column: "ParentId",
                principalTable: "tblBaravordUser",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblBaravordUser_tblBaravordUser_ParentId",
                table: "tblBaravordUser");

            migrationBuilder.DropIndex(
                name: "IX_tblBaravordUser_ParentId",
                table: "tblBaravordUser");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "tblBaravordUser");
        }
    }
}
