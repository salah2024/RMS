using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class c1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Zarib",
                table: "tblFosoul",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblFosoulItem",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FosoulId = table.Column<long>(type: "bigint", nullable: false),
                    FBShomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFosoulItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblFosoulItem_tblFosoul_FosoulId",
                        column: x => x.FosoulId,
                        principalTable: "tblFosoul",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblFosoulItem_FosoulId",
                table: "tblFosoulItem",
                column: "FosoulId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblFosoulItem");

            migrationBuilder.DropColumn(
                name: "Zarib",
                table: "tblFosoul");
        }
    }
}
