using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblFB_tblPolVaAbroBarAvord_BarAvordId",
                table: "tblFB");

            migrationBuilder.AddForeignKey(
                name: "FK_tblFB_tblBaravordUser_BarAvordId",
                table: "tblFB",
                column: "BarAvordId",
                principalTable: "tblBaravordUser",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblFB_tblBaravordUser_BarAvordId",
                table: "tblFB");

            migrationBuilder.AddForeignKey(
                name: "FK_tblFB_tblPolVaAbroBarAvord_BarAvordId",
                table: "tblFB",
                column: "BarAvordId",
                principalTable: "tblPolVaAbroBarAvord",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
