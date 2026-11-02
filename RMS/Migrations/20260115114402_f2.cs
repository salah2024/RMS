using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class f2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PolVaAbroBarAvordRizMetres_tblPolVaAbroBarAvord_PolVaAbroBarAvordId",
                table: "PolVaAbroBarAvordRizMetres");

            migrationBuilder.DropForeignKey(
                name: "FK_PolVaAbroBarAvordRizMetres_tblRizMetreUsers_RizMetreId",
                table: "PolVaAbroBarAvordRizMetres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PolVaAbroBarAvordRizMetres",
                table: "PolVaAbroBarAvordRizMetres");

            migrationBuilder.RenameTable(
                name: "PolVaAbroBarAvordRizMetres",
                newName: "tblPolVaAbroBarAvordRizMetre");

            migrationBuilder.RenameIndex(
                name: "IX_PolVaAbroBarAvordRizMetres_RizMetreId",
                table: "tblPolVaAbroBarAvordRizMetre",
                newName: "IX_tblPolVaAbroBarAvordRizMetre_RizMetreId");

            migrationBuilder.RenameIndex(
                name: "IX_PolVaAbroBarAvordRizMetres_PolVaAbroBarAvordId",
                table: "tblPolVaAbroBarAvordRizMetre",
                newName: "IX_tblPolVaAbroBarAvordRizMetre_PolVaAbroBarAvordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblPolVaAbroBarAvordRizMetre",
                table: "tblPolVaAbroBarAvordRizMetre",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblPolVaAbroBarAvordRizMetre_tblPolVaAbroBarAvord_PolVaAbroBarAvordId",
                table: "tblPolVaAbroBarAvordRizMetre",
                column: "PolVaAbroBarAvordId",
                principalTable: "tblPolVaAbroBarAvord",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblPolVaAbroBarAvordRizMetre_tblRizMetreUsers_RizMetreId",
                table: "tblPolVaAbroBarAvordRizMetre",
                column: "RizMetreId",
                principalTable: "tblRizMetreUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblPolVaAbroBarAvordRizMetre_tblPolVaAbroBarAvord_PolVaAbroBarAvordId",
                table: "tblPolVaAbroBarAvordRizMetre");

            migrationBuilder.DropForeignKey(
                name: "FK_tblPolVaAbroBarAvordRizMetre_tblRizMetreUsers_RizMetreId",
                table: "tblPolVaAbroBarAvordRizMetre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblPolVaAbroBarAvordRizMetre",
                table: "tblPolVaAbroBarAvordRizMetre");

            migrationBuilder.RenameTable(
                name: "tblPolVaAbroBarAvordRizMetre",
                newName: "PolVaAbroBarAvordRizMetres");

            migrationBuilder.RenameIndex(
                name: "IX_tblPolVaAbroBarAvordRizMetre_RizMetreId",
                table: "PolVaAbroBarAvordRizMetres",
                newName: "IX_PolVaAbroBarAvordRizMetres_RizMetreId");

            migrationBuilder.RenameIndex(
                name: "IX_tblPolVaAbroBarAvordRizMetre_PolVaAbroBarAvordId",
                table: "PolVaAbroBarAvordRizMetres",
                newName: "IX_PolVaAbroBarAvordRizMetres_PolVaAbroBarAvordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PolVaAbroBarAvordRizMetres",
                table: "PolVaAbroBarAvordRizMetres",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PolVaAbroBarAvordRizMetres_tblPolVaAbroBarAvord_PolVaAbroBarAvordId",
                table: "PolVaAbroBarAvordRizMetres",
                column: "PolVaAbroBarAvordId",
                principalTable: "tblPolVaAbroBarAvord",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PolVaAbroBarAvordRizMetres_tblRizMetreUsers_RizMetreId",
                table: "PolVaAbroBarAvordRizMetres",
                column: "RizMetreId",
                principalTable: "tblRizMetreUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
