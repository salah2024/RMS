using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a108 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAmalyateKhakiInfoForBarAvordHamlRizMetre");

            migrationBuilder.CreateTable(
                name: "tblAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmalyateKhakiInfoForBarAvordEzafeBahaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RizMetreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre_tblAmalyateKhakiInfoForBarAvordEzafeBaha_AmalyateKhakiInfoForBarAvordEz~",
                        column: x => x.AmalyateKhakiInfoForBarAvordEzafeBahaId,
                        principalTable: "tblAmalyateKhakiInfoForBarAvordEzafeBaha",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre_tblRizMetreUsers_RizMetreId",
                        column: x => x.RizMetreId,
                        principalTable: "tblRizMetreUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblItemsHasConditionAddedToFBHamlRizMetre",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemsHasConditionAddedToFBId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RizMetreUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItemsHasConditionAddedToFBHamlRizMetre", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblItemsHasConditionAddedToFBHamlRizMetre_tblItemsHasConditionAddedToFB_ItemsHasConditionAddedToFBId",
                        column: x => x.ItemsHasConditionAddedToFBId,
                        principalTable: "tblItemsHasConditionAddedToFB",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblItemsHasConditionAddedToFBHamlRizMetre_tblRizMetreUsers_RizMetreUserId",
                        column: x => x.RizMetreUserId,
                        principalTable: "tblRizMetreUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre_AmalyateKhakiInfoForBarAvordEzafeBahaId",
                table: "tblAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre",
                column: "AmalyateKhakiInfoForBarAvordEzafeBahaId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre_RizMetreId",
                table: "tblAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre",
                column: "RizMetreId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItemsHasConditionAddedToFBHamlRizMetre_ItemsHasConditionAddedToFBId",
                table: "tblItemsHasConditionAddedToFBHamlRizMetre",
                column: "ItemsHasConditionAddedToFBId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItemsHasConditionAddedToFBHamlRizMetre_RizMetreUserId",
                table: "tblItemsHasConditionAddedToFBHamlRizMetre",
                column: "RizMetreUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre");

            migrationBuilder.DropTable(
                name: "tblItemsHasConditionAddedToFBHamlRizMetre");

            migrationBuilder.CreateTable(
                name: "tblAmalyateKhakiInfoForBarAvordHamlRizMetre",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmalyateKhakiInfoForBarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RizMetreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
    }
}
