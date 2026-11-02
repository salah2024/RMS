using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class i25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblItemsHasConditionAddedToFBRizMetre",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemsHasConditionAddedToFBId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RizMetreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItemsHasConditionAddedToFBRizMetre", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblItemsHasConditionAddedToFBRizMetre_tblItemsHasConditionAddedToFB_ItemsHasConditionAddedToFBId",
                        column: x => x.ItemsHasConditionAddedToFBId,
                        principalTable: "tblItemsHasConditionAddedToFB",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblItemsHasConditionAddedToFBRizMetre_tblRizMetreUsers_RizMetreId",
                        column: x => x.RizMetreId,
                        principalTable: "tblRizMetreUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblItemsHasConditionAddedToFBRizMetre_ItemsHasConditionAddedToFBId",
                table: "tblItemsHasConditionAddedToFBRizMetre",
                column: "ItemsHasConditionAddedToFBId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItemsHasConditionAddedToFBRizMetre_RizMetreId",
                table: "tblItemsHasConditionAddedToFBRizMetre",
                column: "RizMetreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblItemsHasConditionAddedToFBRizMetre");
        }
    }
}
