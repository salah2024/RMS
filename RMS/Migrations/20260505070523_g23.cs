using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class g23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblFosoulItem");

            migrationBuilder.AddColumn<bool>(
                name: "FaslAsli",
                table: "tblFosoul",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FaslId",
                table: "tblFehrestBahaItemsReplace",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaslAsli",
                table: "tblFosoul");

            migrationBuilder.DropColumn(
                name: "FaslId",
                table: "tblFehrestBahaItemsReplace");

            migrationBuilder.CreateTable(
                name: "tblFosoulItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FosoulId = table.Column<long>(type: "bigint", nullable: false),
                    FBShomareh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFosoulItem", x => x.Id);
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
    }
}
