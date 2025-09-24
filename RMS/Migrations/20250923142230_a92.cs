using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a92 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblKhakRiziBarAvordRizMetre",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KhakRiziBarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RizMetreUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblKhakRiziBarAvordRizMetre", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblKhakRiziBarAvordRizMetre_tblKhakRiziBarAvord_KhakRiziBarAvordId",
                        column: x => x.KhakRiziBarAvordId,
                        principalTable: "tblKhakRiziBarAvord",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblKhakRiziBarAvordRizMetre_tblRizMetreUsers_RizMetreUserId",
                        column: x => x.RizMetreUserId,
                        principalTable: "tblRizMetreUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblKhakRiziBarAvordRizMetre_KhakRiziBarAvordId",
                table: "tblKhakRiziBarAvordRizMetre",
                column: "KhakRiziBarAvordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblKhakRiziBarAvordRizMetre_RizMetreUserId",
                table: "tblKhakRiziBarAvordRizMetre",
                column: "RizMetreUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblKhakRiziBarAvordRizMetre");
        }
    }
}
