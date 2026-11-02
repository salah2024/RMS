using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class h18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaravordUserAccesses_tblBaravordUser_BaravordUserId",
                table: "BaravordUserAccesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaravordUserAccesses",
                table: "BaravordUserAccesses");

            migrationBuilder.RenameTable(
                name: "BaravordUserAccesses",
                newName: "tblBaravordUserAccess");

            migrationBuilder.RenameIndex(
                name: "IX_BaravordUserAccesses_BaravordUserId",
                table: "tblBaravordUserAccess",
                newName: "IX_tblBaravordUserAccess_BaravordUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblBaravordUserAccess",
                table: "tblBaravordUserAccess",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblBaravordUserAccess_tblBaravordUser_BaravordUserId",
                table: "tblBaravordUserAccess",
                column: "BaravordUserId",
                principalTable: "tblBaravordUser",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblBaravordUserAccess_tblBaravordUser_BaravordUserId",
                table: "tblBaravordUserAccess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblBaravordUserAccess",
                table: "tblBaravordUserAccess");

            migrationBuilder.RenameTable(
                name: "tblBaravordUserAccess",
                newName: "BaravordUserAccesses");

            migrationBuilder.RenameIndex(
                name: "IX_tblBaravordUserAccess_BaravordUserId",
                table: "BaravordUserAccesses",
                newName: "IX_BaravordUserAccesses_BaravordUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaravordUserAccesses",
                table: "BaravordUserAccesses",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BaravordUserAccesses_tblBaravordUser_BaravordUserId",
                table: "BaravordUserAccesses",
                column: "BaravordUserId",
                principalTable: "tblBaravordUser",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
