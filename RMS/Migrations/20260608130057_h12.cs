using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class h12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ItemZarib",
                table: "tblFB",
                type: "decimal(5,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_tblItemFBStar_BaravordId",
                table: "tblItemFBStar",
                column: "BaravordId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblItemFBStar_tblBaravordUser_BaravordId",
                table: "tblItemFBStar",
                column: "BaravordId",
                principalTable: "tblBaravordUser",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblItemFBStar_tblBaravordUser_BaravordId",
                table: "tblItemFBStar");

            migrationBuilder.DropIndex(
                name: "IX_tblItemFBStar_BaravordId",
                table: "tblItemFBStar");

            migrationBuilder.DropColumn(
                name: "ItemZarib",
                table: "tblFB");
        }
    }
}
