using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a109 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblItemsHasConditionAddedToFBHamlRizMetre");

            migrationBuilder.CreateTable(
                name: "tblBarAvordHaml",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FBShomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBarAvordHaml", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblBarAvordHaml_tblBaravordUser_BarAvordId",
                        column: x => x.BarAvordId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblBarAvordHamlRizMetre",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarAvordHamlId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RizMetreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBarAvordHamlRizMetre", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblBarAvordHamlRizMetre_tblBarAvordHaml_BarAvordHamlId",
                        column: x => x.BarAvordHamlId,
                        principalTable: "tblBarAvordHaml",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblBarAvordHamlRizMetre_tblRizMetreUsers_RizMetreId",
                        column: x => x.RizMetreId,
                        principalTable: "tblRizMetreUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblBarAvordHaml_BarAvordId",
                table: "tblBarAvordHaml",
                column: "BarAvordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBarAvordHamlRizMetre_BarAvordHamlId",
                table: "tblBarAvordHamlRizMetre",
                column: "BarAvordHamlId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBarAvordHamlRizMetre_RizMetreId",
                table: "tblBarAvordHamlRizMetre",
                column: "RizMetreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblBarAvordHamlRizMetre");

            migrationBuilder.DropTable(
                name: "tblBarAvordHaml");

            migrationBuilder.CreateTable(
                name: "tblItemsHasConditionAddedToFBHamlRizMetre",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemsHasConditionAddedToFBId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RizMetreUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                name: "IX_tblItemsHasConditionAddedToFBHamlRizMetre_ItemsHasConditionAddedToFBId",
                table: "tblItemsHasConditionAddedToFBHamlRizMetre",
                column: "ItemsHasConditionAddedToFBId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItemsHasConditionAddedToFBHamlRizMetre_RizMetreUserId",
                table: "tblItemsHasConditionAddedToFBHamlRizMetre",
                column: "RizMetreUserId");
        }
    }
}
