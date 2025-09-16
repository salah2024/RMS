using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a58 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BarAvordId",
                table: "tblBarAvordAddedBoard",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tblBarAvordAddedBoard_BarAvordId",
                table: "tblBarAvordAddedBoard",
                column: "BarAvordId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblBarAvordAddedBoard_tblBaravordUser_BarAvordId",
                table: "tblBarAvordAddedBoard",
                column: "BarAvordId",
                principalTable: "tblBaravordUser",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblBarAvordAddedBoard_tblBaravordUser_BarAvordId",
                table: "tblBarAvordAddedBoard");

            migrationBuilder.DropIndex(
                name: "IX_tblBarAvordAddedBoard_BarAvordId",
                table: "tblBarAvordAddedBoard");

            migrationBuilder.DropColumn(
                name: "BarAvordId",
                table: "tblBarAvordAddedBoard");
        }
    }
}
