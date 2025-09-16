using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a76 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AmalyateKhakiInfoForBarAvordDetailsRizMetres",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmalyateKhakiInfoForBarAvordDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RizMetreUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmalyateKhakiInfoForBarAvordDetailsRizMetres", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AmalyateKhakiInfoForBarAvordDetailsRizMetres_tblAmalyateKhakiInfoForBarAvordDetails_AmalyateKhakiInfoForBarAvordDetailsId",
                        column: x => x.AmalyateKhakiInfoForBarAvordDetailsId,
                        principalTable: "tblAmalyateKhakiInfoForBarAvordDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmalyateKhakiInfoForBarAvordDetailsRizMetres_tblRizMetreUsers_RizMetreUserId",
                        column: x => x.RizMetreUserId,
                        principalTable: "tblRizMetreUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmalyateKhakiInfoForBarAvordDetailsRizMetres_AmalyateKhakiInfoForBarAvordDetailsId",
                table: "AmalyateKhakiInfoForBarAvordDetailsRizMetres",
                column: "AmalyateKhakiInfoForBarAvordDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_AmalyateKhakiInfoForBarAvordDetailsRizMetres_RizMetreUserId",
                table: "AmalyateKhakiInfoForBarAvordDetailsRizMetres",
                column: "RizMetreUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmalyateKhakiInfoForBarAvordDetailsRizMetres");
        }
    }
}
