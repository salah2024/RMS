using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a106 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblAmalyateKhakiInfoForBarAvordHamlRizMetre",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmalyateKhakiInfoForBarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RizMetreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAmalyateKhakiInfoForBarAvordHamlRizMetre", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblAmalyateKhakiInfoForBarAvordHamlRizMetre_tblAmalyateKhakiInfoForBarAvord_AmalyateKhakiInfoForBarAvordId",
                        column: x => x.AmalyateKhakiInfoForBarAvordId,
                        principalTable: "tblAmalyateKhakiInfoForBarAvord",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblAmalyateKhakiInfoForBarAvordHamlRizMetre_tblRizMetreUsers_RizMetreId",
                        column: x => x.RizMetreId,
                        principalTable: "tblRizMetreUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblAmalyateKhakiInfoForBarAvordHamlRizMetre_AmalyateKhakiInfoForBarAvordId",
                table: "tblAmalyateKhakiInfoForBarAvordHamlRizMetre",
                column: "AmalyateKhakiInfoForBarAvordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAmalyateKhakiInfoForBarAvordHamlRizMetre_RizMetreId",
                table: "tblAmalyateKhakiInfoForBarAvordHamlRizMetre",
                column: "RizMetreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAmalyateKhakiInfoForBarAvordHamlRizMetre");
        }
    }
}
