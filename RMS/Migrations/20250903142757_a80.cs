using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a80 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAmalyateKhakiInfoForBarAvordDetailsEzafeBaha");

            migrationBuilder.CreateTable(
                name: "tblNoeKhakBardariEzafeBaha",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FBItemShomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    hasEnteringValue = table.Column<bool>(type: "bit", nullable: false),
                    CountForEnteringValue = table.Column<int>(type: "int", nullable: false),
                    DesForEnteringValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultForEnteringValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblNoeKhakBardariEzafeBaha", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblAmalyateKhakiInfoForBarAvordEzafeBaha",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmalyateKhakiInfoForBarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoeKhakBardariEzafeBahaId = table.Column<long>(type: "bigint", nullable: false),
                    Tool = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Arz = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAmalyateKhakiInfoForBarAvordEzafeBaha", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblAmalyateKhakiInfoForBarAvordEzafeBaha_tblAmalyateKhakiInfoForBarAvord_AmalyateKhakiInfoForBarAvordId",
                        column: x => x.AmalyateKhakiInfoForBarAvordId,
                        principalTable: "tblAmalyateKhakiInfoForBarAvord",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblAmalyateKhakiInfoForBarAvordEzafeBaha_tblNoeKhakBardariEzafeBaha_NoeKhakBardariEzafeBahaId",
                        column: x => x.NoeKhakBardariEzafeBahaId,
                        principalTable: "tblNoeKhakBardariEzafeBaha",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmalyateKhakiInfoForBarAvordEzafeBahaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RizMetreUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre_tblAmalyateKhakiInfoForBarAvordEzafeBaha_AmalyateKhakiInfoForBarAvordEzafeB~",
                        column: x => x.AmalyateKhakiInfoForBarAvordEzafeBahaId,
                        principalTable: "tblAmalyateKhakiInfoForBarAvordEzafeBaha",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre_tblRizMetreUsers_RizMetreUserId",
                        column: x => x.RizMetreUserId,
                        principalTable: "tblRizMetreUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblAmalyateKhakiInfoForBarAvordEzafeBaha_AmalyateKhakiInfoForBarAvordId",
                table: "tblAmalyateKhakiInfoForBarAvordEzafeBaha",
                column: "AmalyateKhakiInfoForBarAvordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAmalyateKhakiInfoForBarAvordEzafeBaha_NoeKhakBardariEzafeBahaId",
                table: "tblAmalyateKhakiInfoForBarAvordEzafeBaha",
                column: "NoeKhakBardariEzafeBahaId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre_AmalyateKhakiInfoForBarAvordEzafeBahaId",
                table: "tblAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre",
                column: "AmalyateKhakiInfoForBarAvordEzafeBahaId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre_RizMetreUserId",
                table: "tblAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre",
                column: "RizMetreUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre");

            migrationBuilder.DropTable(
                name: "tblAmalyateKhakiInfoForBarAvordEzafeBaha");

            migrationBuilder.DropTable(
                name: "tblNoeKhakBardariEzafeBaha");

            migrationBuilder.CreateTable(
                name: "tblAmalyateKhakiInfoForBarAvordDetailsEzafeBaha",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmalyateKhakiInfoForBarAvordDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Value = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAmalyateKhakiInfoForBarAvordDetailsEzafeBaha", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblAmalyateKhakiInfoForBarAvordDetailsEzafeBaha_tblAmalyateKhakiInfoForBarAvordDetails_AmalyateKhakiInfoForBarAvordDetailsId",
                        column: x => x.AmalyateKhakiInfoForBarAvordDetailsId,
                        principalTable: "tblAmalyateKhakiInfoForBarAvordDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblAmalyateKhakiInfoForBarAvordDetailsEzafeBaha_AmalyateKhakiInfoForBarAvordDetailsId",
                table: "tblAmalyateKhakiInfoForBarAvordDetailsEzafeBaha",
                column: "AmalyateKhakiInfoForBarAvordDetailsId");
        }
    }
}
