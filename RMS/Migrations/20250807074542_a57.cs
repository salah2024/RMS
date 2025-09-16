using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a57 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblRizMetreForBarAvordAddedBoard",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarAvordAddedBoardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RizMetreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRizMetreForBarAvordAddedBoard", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblRizMetreForBarAvordAddedBoard_tblBarAvordAddedBoard_BarAvordAddedBoardId",
                        column: x => x.BarAvordAddedBoardId,
                        principalTable: "tblBarAvordAddedBoard",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblRizMetreForBarAvordAddedBoard_tblRizMetreUsers_RizMetreId",
                        column: x => x.RizMetreId,
                        principalTable: "tblRizMetreUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblRizMetreForBarAvordAddedBoard_BarAvordAddedBoardId",
                table: "tblRizMetreForBarAvordAddedBoard",
                column: "BarAvordAddedBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRizMetreForBarAvordAddedBoard_RizMetreId",
                table: "tblRizMetreForBarAvordAddedBoard",
                column: "RizMetreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblRizMetreForBarAvordAddedBoard");
        }
    }
}
