using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class i29 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblBarAvordKarfarma",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KarFarmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarAvordUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBarAvordKarfarma", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblBarAvordKarfarma_tblBaravordUser_BarAvordUserId",
                        column: x => x.BarAvordUserId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblBarAvordKarfarma_tblCompany_KarFarmaId",
                        column: x => x.KarFarmaId,
                        principalTable: "tblCompany",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblBarAvordMoshaver",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MoshaverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarAvordUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBarAvordMoshaver", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblBarAvordMoshaver_tblBaravordUser_BarAvordUserId",
                        column: x => x.BarAvordUserId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblBarAvordMoshaver_tblCompany_MoshaverId",
                        column: x => x.MoshaverId,
                        principalTable: "tblCompany",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblBarAvordPaymanKar",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayManKarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarAvordUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBarAvordPaymanKar", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblBarAvordPaymanKar_tblBaravordUser_BarAvordUserId",
                        column: x => x.BarAvordUserId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblBarAvordPaymanKar_tblCompany_PayManKarId",
                        column: x => x.PayManKarId,
                        principalTable: "tblCompany",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblBarAvordKarfarma_BarAvordUserId",
                table: "tblBarAvordKarfarma",
                column: "BarAvordUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBarAvordKarfarma_KarFarmaId",
                table: "tblBarAvordKarfarma",
                column: "KarFarmaId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBarAvordMoshaver_BarAvordUserId",
                table: "tblBarAvordMoshaver",
                column: "BarAvordUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBarAvordMoshaver_MoshaverId",
                table: "tblBarAvordMoshaver",
                column: "MoshaverId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBarAvordPaymanKar_BarAvordUserId",
                table: "tblBarAvordPaymanKar",
                column: "BarAvordUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBarAvordPaymanKar_PayManKarId",
                table: "tblBarAvordPaymanKar",
                column: "PayManKarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblBarAvordKarfarma");

            migrationBuilder.DropTable(
                name: "tblBarAvordMoshaver");

            migrationBuilder.DropTable(
                name: "tblBarAvordPaymanKar");
        }
    }
}
