using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class h1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblZaribPishnahadiPaymanKar",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarAvordUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaravordFasl = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BaravordFaslBaZarib = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PishnahadPaymanKar = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ZaribJoz = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NoeFBId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblZaribPishnahadiPaymanKar", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblZaribPishnahadiPaymanKar_tblBaravordUser_BarAvordUserId",
                        column: x => x.BarAvordUserId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblZaribPishnahadiPaymanKar_BarAvordUserId",
                table: "tblZaribPishnahadiPaymanKar",
                column: "BarAvordUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblZaribPishnahadiPaymanKar");
        }
    }
}
