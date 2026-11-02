using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class f24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblTajhizKargahDarsad",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarAvordUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoeFB = table.Column<int>(type: "int", nullable: false),
                    Darsad = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Meghdar = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTajhizKargahDarsad", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblTajhizKargahDarsad_tblBaravordUser_BarAvordUserId",
                        column: x => x.BarAvordUserId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblTajhizKargahDarsad_BarAvordUserId",
                table: "tblTajhizKargahDarsad",
                column: "BarAvordUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblTajhizKargahDarsad");
        }
    }
}
