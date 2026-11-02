using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class h2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaravordFasl",
                table: "tblZaribPishnahadiPaymanKar");

            migrationBuilder.DropColumn(
                name: "BaravordFaslBaZarib",
                table: "tblZaribPishnahadiPaymanKar");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "tblZaribPishnahadiPaymanKar");

            migrationBuilder.DropColumn(
                name: "PishnahadPaymanKar",
                table: "tblZaribPishnahadiPaymanKar");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "tblZaribPishnahadiPaymanKar");

            migrationBuilder.RenameColumn(
                name: "ZaribJoz",
                table: "tblZaribPishnahadiPaymanKar",
                newName: "PishnahadPaymanKarTajhiz");

            migrationBuilder.AddColumn<decimal>(
                name: "ZaribJozTajhiz",
                table: "tblZaribPishnahadiPaymanKar",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblZaribPishnahadiPaymanKarFosoul",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZaribPishnahadiPaymanKarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaravordFasl = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BaravordFaslBaZarib = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PishnahadPaymanKar = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ZaribJoz = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblZaribPishnahadiPaymanKarFosoul", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblZaribPishnahadiPaymanKarFosoul_tblZaribPishnahadiPaymanKar_ZaribPishnahadiPaymanKarId",
                        column: x => x.ZaribPishnahadiPaymanKarId,
                        principalTable: "tblZaribPishnahadiPaymanKar",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblZaribPishnahadiPaymanKarFosoul_ZaribPishnahadiPaymanKarId",
                table: "tblZaribPishnahadiPaymanKarFosoul",
                column: "ZaribPishnahadiPaymanKarId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblZaribPishnahadiPaymanKarFosoul");

            migrationBuilder.DropColumn(
                name: "ZaribJozTajhiz",
                table: "tblZaribPishnahadiPaymanKar");

            migrationBuilder.RenameColumn(
                name: "PishnahadPaymanKarTajhiz",
                table: "tblZaribPishnahadiPaymanKar",
                newName: "ZaribJoz");

            migrationBuilder.AddColumn<decimal>(
                name: "BaravordFasl",
                table: "tblZaribPishnahadiPaymanKar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BaravordFaslBaZarib",
                table: "tblZaribPishnahadiPaymanKar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "tblZaribPishnahadiPaymanKar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PishnahadPaymanKar",
                table: "tblZaribPishnahadiPaymanKar",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "tblZaribPishnahadiPaymanKar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
