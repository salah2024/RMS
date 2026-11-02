using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class i10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblBarAvordTajmie",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TajmieFosoulItemId = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BahayeVahed = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BahayeKol = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBarAvordTajmie", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblBarAvordTajmie_tblBaravordUser_BarAvordId",
                        column: x => x.BarAvordId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblBarAvordTajmie_tblTajmieFosoulItems_TajmieFosoulItemId",
                        column: x => x.TajmieFosoulItemId,
                        principalTable: "tblTajmieFosoulItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblBarAvordTajmie_BarAvordId",
                table: "tblBarAvordTajmie",
                column: "BarAvordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBarAvordTajmie_TajmieFosoulItemId",
                table: "tblBarAvordTajmie",
                column: "TajmieFosoulItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblBarAvordTajmie");
        }
    }
}
