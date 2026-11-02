using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class i1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblTajmieFosoul",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoeFBId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTajmieFosoul", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblTajmieFosoulItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TajmieFosoulId = table.Column<long>(type: "bigint", nullable: false),
                    calculateValueType = table.Column<int>(type: "int", nullable: false),
                    MainItem = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTajmieFosoulItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblTajmieFosoulItems_tblTajmieFosoul_TajmieFosoulId",
                        column: x => x.TajmieFosoulId,
                        principalTable: "tblTajmieFosoul",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblBarAvordUserTajmie",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarAvordUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TajmieFosoulItemId = table.Column<long>(type: "bigint", nullable: false),
                    BahayeVahed = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Meghdar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BahayeKol = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBarAvordUserTajmie", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblBarAvordUserTajmie_tblBaravordUser_BarAvordUserId",
                        column: x => x.BarAvordUserId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblBarAvordUserTajmie_tblTajmieFosoulItems_TajmieFosoulItemId",
                        column: x => x.TajmieFosoulItemId,
                        principalTable: "tblTajmieFosoulItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblTajmieFBItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TajmieFosoulItemId = table.Column<long>(type: "bigint", nullable: false),
                    ItemFBShomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<long>(type: "bigint", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTajmieFBItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblTajmieFBItems_tblTajmieFosoulItems_TajmieFosoulItemId",
                        column: x => x.TajmieFosoulItemId,
                        principalTable: "tblTajmieFosoulItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblBarAvordUserTajmie_BarAvordUserId",
                table: "tblBarAvordUserTajmie",
                column: "BarAvordUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBarAvordUserTajmie_TajmieFosoulItemId",
                table: "tblBarAvordUserTajmie",
                column: "TajmieFosoulItemId");

            migrationBuilder.CreateIndex(
                name: "IX_tblTajmieFBItems_TajmieFosoulItemId",
                table: "tblTajmieFBItems",
                column: "TajmieFosoulItemId");

            migrationBuilder.CreateIndex(
                name: "IX_tblTajmieFosoulItems_TajmieFosoulId",
                table: "tblTajmieFosoulItems",
                column: "TajmieFosoulId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblBarAvordUserTajmie");

            migrationBuilder.DropTable(
                name: "tblTajmieFBItems");

            migrationBuilder.DropTable(
                name: "tblTajmieFosoulItems");

            migrationBuilder.DropTable(
                name: "tblTajmieFosoul");
        }
    }
}
