using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class g17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zarib",
                table: "tblFosoul");

            migrationBuilder.CreateTable(
                name: "tblZarayebFosoul",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FaslId = table.Column<long>(type: "bigint", nullable: false),
                    ZaribBalaSari = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    ZaribManteghe = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblZarayebFosoul", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblZarayebFosoul_tblFosoul_FaslId",
                        column: x => x.FaslId,
                        principalTable: "tblFosoul",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblZarayebFosoul_FaslId",
                table: "tblZarayebFosoul",
                column: "FaslId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblZarayebFosoul");

            migrationBuilder.AddColumn<decimal>(
                name: "Zarib",
                table: "tblFosoul",
                type: "decimal(18,4)",
                nullable: true);
        }
    }
}
