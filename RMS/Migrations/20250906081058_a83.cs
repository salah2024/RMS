using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a83 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmalyateKhakiInfoForBarAvordDetailsRizMetres_tblAmalyateKhakiInfoForBarAvordDetails_AmalyateKhakiInfoForBarAvordDetailsId",
                table: "AmalyateKhakiInfoForBarAvordDetailsRizMetres");

            migrationBuilder.DropForeignKey(
                name: "FK_AmalyateKhakiInfoForBarAvordDetailsRizMetres_tblRizMetreUsers_RizMetreUserId",
                table: "AmalyateKhakiInfoForBarAvordDetailsRizMetres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AmalyateKhakiInfoForBarAvordDetailsRizMetres",
                table: "AmalyateKhakiInfoForBarAvordDetailsRizMetres");

            migrationBuilder.RenameTable(
                name: "AmalyateKhakiInfoForBarAvordDetailsRizMetres",
                newName: "tblAmalyateKhakiInfoForBarAvordDetailsRizMetre");

            migrationBuilder.RenameIndex(
                name: "IX_AmalyateKhakiInfoForBarAvordDetailsRizMetres_RizMetreUserId",
                table: "tblAmalyateKhakiInfoForBarAvordDetailsRizMetre",
                newName: "IX_tblAmalyateKhakiInfoForBarAvordDetailsRizMetre_RizMetreUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AmalyateKhakiInfoForBarAvordDetailsRizMetres_AmalyateKhakiInfoForBarAvordDetailsId",
                table: "tblAmalyateKhakiInfoForBarAvordDetailsRizMetre",
                newName: "IX_tblAmalyateKhakiInfoForBarAvordDetailsRizMetre_AmalyateKhakiInfoForBarAvordDetailsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblAmalyateKhakiInfoForBarAvordDetailsRizMetre",
                table: "tblAmalyateKhakiInfoForBarAvordDetailsRizMetre",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblAmalyateKhakiInfoForBarAvordDetailsRizMetre_tblAmalyateKhakiInfoForBarAvordDetails_AmalyateKhakiInfoForBarAvordDetailsId",
                table: "tblAmalyateKhakiInfoForBarAvordDetailsRizMetre",
                column: "AmalyateKhakiInfoForBarAvordDetailsId",
                principalTable: "tblAmalyateKhakiInfoForBarAvordDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblAmalyateKhakiInfoForBarAvordDetailsRizMetre_tblRizMetreUsers_RizMetreUserId",
                table: "tblAmalyateKhakiInfoForBarAvordDetailsRizMetre",
                column: "RizMetreUserId",
                principalTable: "tblRizMetreUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblAmalyateKhakiInfoForBarAvordDetailsRizMetre_tblAmalyateKhakiInfoForBarAvordDetails_AmalyateKhakiInfoForBarAvordDetailsId",
                table: "tblAmalyateKhakiInfoForBarAvordDetailsRizMetre");

            migrationBuilder.DropForeignKey(
                name: "FK_tblAmalyateKhakiInfoForBarAvordDetailsRizMetre_tblRizMetreUsers_RizMetreUserId",
                table: "tblAmalyateKhakiInfoForBarAvordDetailsRizMetre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblAmalyateKhakiInfoForBarAvordDetailsRizMetre",
                table: "tblAmalyateKhakiInfoForBarAvordDetailsRizMetre");

            migrationBuilder.RenameTable(
                name: "tblAmalyateKhakiInfoForBarAvordDetailsRizMetre",
                newName: "AmalyateKhakiInfoForBarAvordDetailsRizMetres");

            migrationBuilder.RenameIndex(
                name: "IX_tblAmalyateKhakiInfoForBarAvordDetailsRizMetre_RizMetreUserId",
                table: "AmalyateKhakiInfoForBarAvordDetailsRizMetres",
                newName: "IX_AmalyateKhakiInfoForBarAvordDetailsRizMetres_RizMetreUserId");

            migrationBuilder.RenameIndex(
                name: "IX_tblAmalyateKhakiInfoForBarAvordDetailsRizMetre_AmalyateKhakiInfoForBarAvordDetailsId",
                table: "AmalyateKhakiInfoForBarAvordDetailsRizMetres",
                newName: "IX_AmalyateKhakiInfoForBarAvordDetailsRizMetres_AmalyateKhakiInfoForBarAvordDetailsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AmalyateKhakiInfoForBarAvordDetailsRizMetres",
                table: "AmalyateKhakiInfoForBarAvordDetailsRizMetres",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AmalyateKhakiInfoForBarAvordDetailsRizMetres_tblAmalyateKhakiInfoForBarAvordDetails_AmalyateKhakiInfoForBarAvordDetailsId",
                table: "AmalyateKhakiInfoForBarAvordDetailsRizMetres",
                column: "AmalyateKhakiInfoForBarAvordDetailsId",
                principalTable: "tblAmalyateKhakiInfoForBarAvordDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AmalyateKhakiInfoForBarAvordDetailsRizMetres_tblRizMetreUsers_RizMetreUserId",
                table: "AmalyateKhakiInfoForBarAvordDetailsRizMetres",
                column: "RizMetreUserId",
                principalTable: "tblRizMetreUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
