using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class c7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ZaribBalasari",
                table: "tblBaravordUser",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ZaribManteghe",
                table: "tblBaravordUser",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblBaravordZaribBalaSari",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaravordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZaribBalasari = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Tarh = table.Column<int>(type: "int", nullable: false),
                    Monaghese = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBaravordZaribBalaSari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBaravordZaribBalaSari_tblBaravordUser_BaravordId",
                        column: x => x.BaravordId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblBaravordZaribManteghe",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaravordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZaribManteghe = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Ostan = table.Column<long>(type: "bigint", nullable: false),
                    Shahr = table.Column<long>(type: "bigint", nullable: false),
                    Bakhsh = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBaravordZaribManteghe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBaravordZaribManteghe_tblBaravordUser_BaravordId",
                        column: x => x.BaravordId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblBaravordZaribBalaSari_BaravordId",
                table: "tblBaravordZaribBalaSari",
                column: "BaravordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBaravordZaribManteghe_BaravordId",
                table: "tblBaravordZaribManteghe",
                column: "BaravordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblBaravordZaribBalaSari");

            migrationBuilder.DropTable(
                name: "tblBaravordZaribManteghe");

            migrationBuilder.DropColumn(
                name: "ZaribBalasari",
                table: "tblBaravordUser");

            migrationBuilder.DropColumn(
                name: "ZaribManteghe",
                table: "tblBaravordUser");
        }
    }
}
