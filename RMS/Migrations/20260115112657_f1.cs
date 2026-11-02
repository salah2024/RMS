using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class f1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PolVaAbroBarAvordRizMetres",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolVaAbroBarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RizMetreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolDetailType = table.Column<int>(type: "int", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolVaAbroBarAvordRizMetres", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PolVaAbroBarAvordRizMetres_tblPolVaAbroBarAvord_PolVaAbroBarAvordId",
                        column: x => x.PolVaAbroBarAvordId,
                        principalTable: "tblPolVaAbroBarAvord",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PolVaAbroBarAvordRizMetres_tblRizMetreUsers_RizMetreId",
                        column: x => x.RizMetreId,
                        principalTable: "tblRizMetreUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PolVaAbroBarAvordRizMetres_PolVaAbroBarAvordId",
                table: "PolVaAbroBarAvordRizMetres",
                column: "PolVaAbroBarAvordId");

            migrationBuilder.CreateIndex(
                name: "IX_PolVaAbroBarAvordRizMetres_RizMetreId",
                table: "PolVaAbroBarAvordRizMetres",
                column: "RizMetreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PolVaAbroBarAvordRizMetres");
        }
    }
}
