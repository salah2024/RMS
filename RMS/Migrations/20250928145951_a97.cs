using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a97 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhakRiziEzafeBahaBarAvordRizMetres",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KhakRiziEzafeBahaBarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RizMetreUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhakRiziEzafeBahaBarAvordRizMetres", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KhakRiziEzafeBahaBarAvordRizMetres_tblKhakRiziEzafeBahaBarAvord_KhakRiziEzafeBahaBarAvordId",
                        column: x => x.KhakRiziEzafeBahaBarAvordId,
                        principalTable: "tblKhakRiziEzafeBahaBarAvord",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KhakRiziEzafeBahaBarAvordRizMetres_tblRizMetreUsers_RizMetreUserId",
                        column: x => x.RizMetreUserId,
                        principalTable: "tblRizMetreUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KhakRiziEzafeBahaBarAvordRizMetres_KhakRiziEzafeBahaBarAvordId",
                table: "KhakRiziEzafeBahaBarAvordRizMetres",
                column: "KhakRiziEzafeBahaBarAvordId");

            migrationBuilder.CreateIndex(
                name: "IX_KhakRiziEzafeBahaBarAvordRizMetres_RizMetreUserId",
                table: "KhakRiziEzafeBahaBarAvordRizMetres",
                column: "RizMetreUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KhakRiziEzafeBahaBarAvordRizMetres");
        }
    }
}
