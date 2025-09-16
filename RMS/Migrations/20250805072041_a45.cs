using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a45 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblBarAvordAddedBoard",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoardType = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: true),
                    Material = table.Column<int>(type: "int", nullable: true),
                    BoardTip = table.Column<int>(type: "int", nullable: true),
                    PrintType = table.Column<int>(type: "int", nullable: true),
                    MeghdarPrintType = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Tool = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Arz = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RizMetreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBarAvordAddedBoard", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblBarAvordAddedBoard");
        }
    }
}
