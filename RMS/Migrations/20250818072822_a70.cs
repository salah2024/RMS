using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a70 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblBarAvordAddedBoardStand",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoardStandItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tedad = table.Column<int>(type: "int", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBarAvordAddedBoardStand", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblBarAvordAddedBoardStand_tblBaravordUser_BarAvordId",
                        column: x => x.BarAvordId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblBarAvordAddedBoardStand_tblBoardStandItems_BoardStandItemId",
                        column: x => x.BoardStandItemId,
                        principalTable: "tblBoardStandItems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblRizMetreForBarAvordAddedBoardStand",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarAvordAddedBoardStandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RizMetreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRizMetreForBarAvordAddedBoardStand", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblRizMetreForBarAvordAddedBoardStand_tblBarAvordAddedBoardStand_BarAvordAddedBoardStandId",
                        column: x => x.BarAvordAddedBoardStandId,
                        principalTable: "tblBarAvordAddedBoardStand",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblRizMetreForBarAvordAddedBoardStand_tblRizMetreUsers_RizMetreId",
                        column: x => x.RizMetreId,
                        principalTable: "tblRizMetreUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblBarAvordAddedBoardStand_BarAvordId",
                table: "tblBarAvordAddedBoardStand",
                column: "BarAvordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBarAvordAddedBoardStand_BoardStandItemId",
                table: "tblBarAvordAddedBoardStand",
                column: "BoardStandItemId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRizMetreForBarAvordAddedBoardStand_BarAvordAddedBoardStandId",
                table: "tblRizMetreForBarAvordAddedBoardStand",
                column: "BarAvordAddedBoardStandId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRizMetreForBarAvordAddedBoardStand_RizMetreId",
                table: "tblRizMetreForBarAvordAddedBoardStand",
                column: "RizMetreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblRizMetreForBarAvordAddedBoardStand");

            migrationBuilder.DropTable(
                name: "tblBarAvordAddedBoardStand");
        }
    }
}
