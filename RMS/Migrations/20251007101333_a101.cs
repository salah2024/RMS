using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a101 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblPayKaniInfoForBarAvord",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaravordUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromKM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToKM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KMNum = table.Column<int>(type: "int", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPayKaniInfoForBarAvord", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblPayKaniInfoForBarAvord_tblBaravordUser_BaravordUserId",
                        column: x => x.BaravordUserId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblPayKaniInfoForBarAvordDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayKaniInfoForBarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoeKhakBardariId = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    boolValue = table.Column<bool>(type: "bit", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPayKaniInfoForBarAvordDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblPayKaniInfoForBarAvordDetails_tblNoeKhakBardari_NoeKhakBardariId",
                        column: x => x.NoeKhakBardariId,
                        principalTable: "tblNoeKhakBardari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblPayKaniInfoForBarAvordDetails_tblPayKaniInfoForBarAvord_PayKaniInfoForBarAvordId",
                        column: x => x.PayKaniInfoForBarAvordId,
                        principalTable: "tblPayKaniInfoForBarAvord",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblPayKaniInfoForBarAvordEzafeBaha",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayKaniInfoForBarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoeKhakBardariEzafeBahaId = table.Column<long>(type: "bigint", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPayKaniInfoForBarAvordEzafeBaha", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblPayKaniInfoForBarAvordEzafeBaha_tblNoeKhakBardariEzafeBaha_NoeKhakBardariEzafeBahaId",
                        column: x => x.NoeKhakBardariEzafeBahaId,
                        principalTable: "tblNoeKhakBardariEzafeBaha",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblPayKaniInfoForBarAvordEzafeBaha_tblPayKaniInfoForBarAvord_PayKaniInfoForBarAvordId",
                        column: x => x.PayKaniInfoForBarAvordId,
                        principalTable: "tblPayKaniInfoForBarAvord",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblPayKaniInfoForBarAvordMore",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayKaniInfoForBarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPayKaniInfoForBarAvordMore", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblPayKaniInfoForBarAvordMore_tblPayKaniInfoForBarAvord_PayKaniInfoForBarAvordId",
                        column: x => x.PayKaniInfoForBarAvordId,
                        principalTable: "tblPayKaniInfoForBarAvord",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblPayKaniInfoForBarAvordDetailsMore",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayKaniInfoForBarAvordDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPayKaniInfoForBarAvordDetailsMore", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblPayKaniInfoForBarAvordDetailsMore_tblPayKaniInfoForBarAvordDetails_PayKaniInfoForBarAvordDetailsId",
                        column: x => x.PayKaniInfoForBarAvordDetailsId,
                        principalTable: "tblPayKaniInfoForBarAvordDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblPayKaniInfoForBarAvordDetailsRizMetre",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayKaniInfoForBarAvordDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RizMetreUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPayKaniInfoForBarAvordDetailsRizMetre", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblPayKaniInfoForBarAvordDetailsRizMetre_tblPayKaniInfoForBarAvordDetails_PayKaniInfoForBarAvordDetailsId",
                        column: x => x.PayKaniInfoForBarAvordDetailsId,
                        principalTable: "tblPayKaniInfoForBarAvordDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblPayKaniInfoForBarAvordDetailsRizMetre_tblRizMetreUsers_RizMetreUserId",
                        column: x => x.RizMetreUserId,
                        principalTable: "tblRizMetreUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblPayKaniInfoForBarAvordEzafeBahaRizMetre",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayKaniInfoForBarAvordEzafeBahaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RizMetreUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPayKaniInfoForBarAvordEzafeBahaRizMetre", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblPayKaniInfoForBarAvordEzafeBahaRizMetre_tblPayKaniInfoForBarAvordEzafeBaha_PayKaniInfoForBarAvordEzafeBahaId",
                        column: x => x.PayKaniInfoForBarAvordEzafeBahaId,
                        principalTable: "tblPayKaniInfoForBarAvordEzafeBaha",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblPayKaniInfoForBarAvordEzafeBahaRizMetre_tblRizMetreUsers_RizMetreUserId",
                        column: x => x.RizMetreUserId,
                        principalTable: "tblRizMetreUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblPayKaniInfoForBarAvord_BaravordUserId",
                table: "tblPayKaniInfoForBarAvord",
                column: "BaravordUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPayKaniInfoForBarAvordDetails_NoeKhakBardariId",
                table: "tblPayKaniInfoForBarAvordDetails",
                column: "NoeKhakBardariId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPayKaniInfoForBarAvordDetails_PayKaniInfoForBarAvordId",
                table: "tblPayKaniInfoForBarAvordDetails",
                column: "PayKaniInfoForBarAvordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPayKaniInfoForBarAvordDetailsMore_PayKaniInfoForBarAvordDetailsId",
                table: "tblPayKaniInfoForBarAvordDetailsMore",
                column: "PayKaniInfoForBarAvordDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPayKaniInfoForBarAvordDetailsRizMetre_PayKaniInfoForBarAvordDetailsId",
                table: "tblPayKaniInfoForBarAvordDetailsRizMetre",
                column: "PayKaniInfoForBarAvordDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPayKaniInfoForBarAvordDetailsRizMetre_RizMetreUserId",
                table: "tblPayKaniInfoForBarAvordDetailsRizMetre",
                column: "RizMetreUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPayKaniInfoForBarAvordEzafeBaha_NoeKhakBardariEzafeBahaId",
                table: "tblPayKaniInfoForBarAvordEzafeBaha",
                column: "NoeKhakBardariEzafeBahaId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPayKaniInfoForBarAvordEzafeBaha_PayKaniInfoForBarAvordId",
                table: "tblPayKaniInfoForBarAvordEzafeBaha",
                column: "PayKaniInfoForBarAvordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPayKaniInfoForBarAvordEzafeBahaRizMetre_PayKaniInfoForBarAvordEzafeBahaId",
                table: "tblPayKaniInfoForBarAvordEzafeBahaRizMetre",
                column: "PayKaniInfoForBarAvordEzafeBahaId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPayKaniInfoForBarAvordEzafeBahaRizMetre_RizMetreUserId",
                table: "tblPayKaniInfoForBarAvordEzafeBahaRizMetre",
                column: "RizMetreUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPayKaniInfoForBarAvordMore_PayKaniInfoForBarAvordId",
                table: "tblPayKaniInfoForBarAvordMore",
                column: "PayKaniInfoForBarAvordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblPayKaniInfoForBarAvordDetailsMore");

            migrationBuilder.DropTable(
                name: "tblPayKaniInfoForBarAvordDetailsRizMetre");

            migrationBuilder.DropTable(
                name: "tblPayKaniInfoForBarAvordEzafeBahaRizMetre");

            migrationBuilder.DropTable(
                name: "tblPayKaniInfoForBarAvordMore");

            migrationBuilder.DropTable(
                name: "tblPayKaniInfoForBarAvordDetails");

            migrationBuilder.DropTable(
                name: "tblPayKaniInfoForBarAvordEzafeBaha");

            migrationBuilder.DropTable(
                name: "tblPayKaniInfoForBarAvord");
        }
    }
}
