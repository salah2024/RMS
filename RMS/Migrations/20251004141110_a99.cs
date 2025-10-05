using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a99 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KhakRiziEzafeBahaBarAvordRizMetres");

            migrationBuilder.DropTable(
                name: "tblKhakRiziEzafeBahaBarAvord");

            migrationBuilder.CreateTable(
                name: "tblKhakRiziEzafeBaha",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KhakRiziBarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EzafeBahaKhakRiziId = table.Column<long>(type: "bigint", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblKhakRiziEzafeBaha", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblKhakRiziEzafeBaha_tblEzafeBahaKhakRizi_EzafeBahaKhakRiziId",
                        column: x => x.EzafeBahaKhakRiziId,
                        principalTable: "tblEzafeBahaKhakRizi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblKhakRiziEzafeBaha_tblKhakRiziBarAvord_KhakRiziBarAvordId",
                        column: x => x.KhakRiziBarAvordId,
                        principalTable: "tblKhakRiziBarAvord",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblKhakRiziEzafeBahasRizMetre",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KhakRiziEzafeBahaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RizMetreUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblKhakRiziEzafeBahasRizMetre", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblKhakRiziEzafeBahasRizMetre_tblKhakRiziEzafeBaha_KhakRiziEzafeBahaId",
                        column: x => x.KhakRiziEzafeBahaId,
                        principalTable: "tblKhakRiziEzafeBaha",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblKhakRiziEzafeBahasRizMetre_tblRizMetreUsers_RizMetreUserId",
                        column: x => x.RizMetreUserId,
                        principalTable: "tblRizMetreUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblKhakRiziEzafeBaha_EzafeBahaKhakRiziId",
                table: "tblKhakRiziEzafeBaha",
                column: "EzafeBahaKhakRiziId");

            migrationBuilder.CreateIndex(
                name: "IX_tblKhakRiziEzafeBaha_KhakRiziBarAvordId",
                table: "tblKhakRiziEzafeBaha",
                column: "KhakRiziBarAvordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblKhakRiziEzafeBahasRizMetre_KhakRiziEzafeBahaId",
                table: "tblKhakRiziEzafeBahasRizMetre",
                column: "KhakRiziEzafeBahaId");

            migrationBuilder.CreateIndex(
                name: "IX_tblKhakRiziEzafeBahasRizMetre_RizMetreUserId",
                table: "tblKhakRiziEzafeBahasRizMetre",
                column: "RizMetreUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblKhakRiziEzafeBahasRizMetre");

            migrationBuilder.DropTable(
                name: "tblKhakRiziEzafeBaha");

            migrationBuilder.CreateTable(
                name: "tblKhakRiziEzafeBahaBarAvord",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EzafeBahaKhakRiziId = table.Column<long>(type: "bigint", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblKhakRiziEzafeBahaBarAvord", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblKhakRiziEzafeBahaBarAvord_tblBaravordUser_BarAvordId",
                        column: x => x.BarAvordId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblKhakRiziEzafeBahaBarAvord_tblEzafeBahaKhakRizi_EzafeBahaKhakRiziId",
                        column: x => x.EzafeBahaKhakRiziId,
                        principalTable: "tblEzafeBahaKhakRizi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KhakRiziEzafeBahaBarAvordRizMetres",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KhakRiziEzafeBahaBarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RizMetreUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_tblKhakRiziEzafeBahaBarAvord_BarAvordId",
                table: "tblKhakRiziEzafeBahaBarAvord",
                column: "BarAvordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblKhakRiziEzafeBahaBarAvord_EzafeBahaKhakRiziId",
                table: "tblKhakRiziEzafeBahaBarAvord",
                column: "EzafeBahaKhakRiziId");
        }
    }
}
