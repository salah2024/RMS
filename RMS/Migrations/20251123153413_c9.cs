using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class c9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblVahed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblVahed", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblItemFBStar",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaravordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Shomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BahayeVahed = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VahedId = table.Column<int>(type: "int", nullable: false),
                    Sharh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItemFBStar", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblItemFBStar_tblVahed_VahedId",
                        column: x => x.VahedId,
                        principalTable: "tblVahed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblRizMetreStar",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Shomareh = table.Column<long>(type: "bigint", nullable: false),
                    ShomarehNew = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sharh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tedad = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Tool = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Arz = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Ertefa = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Vazn = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    MeghdarJoz = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Des = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemFBStarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRizMetreStar", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblRizMetreStar_tblItemFBStar_ItemFBStarId",
                        column: x => x.ItemFBStarId,
                        principalTable: "tblItemFBStar",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblItemFBStar_VahedId",
                table: "tblItemFBStar",
                column: "VahedId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRizMetreStar_ItemFBStarId",
                table: "tblRizMetreStar",
                column: "ItemFBStarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblRizMetreStar");

            migrationBuilder.DropTable(
                name: "tblItemFBStar");

            migrationBuilder.DropTable(
                name: "tblVahed");
        }
    }
}
