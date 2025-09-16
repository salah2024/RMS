using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a82 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblNoeKhakBardari_NoeKhakBardariEzafeBaha",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoeKhakBardariId = table.Column<long>(type: "bigint", nullable: false),
                    NoeKhakBardariEzafeBahaId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblNoeKhakBardari_NoeKhakBardariEzafeBaha", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblNoeKhakBardari_NoeKhakBardariEzafeBaha_tblNoeKhakBardariEzafeBaha_NoeKhakBardariEzafeBahaId",
                        column: x => x.NoeKhakBardariEzafeBahaId,
                        principalTable: "tblNoeKhakBardariEzafeBaha",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblNoeKhakBardari_NoeKhakBardariEzafeBaha_tblNoeKhakBardari_NoeKhakBardariId",
                        column: x => x.NoeKhakBardariId,
                        principalTable: "tblNoeKhakBardari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblNoeKhakBardari_NoeKhakBardariEzafeBaha_NoeKhakBardariEzafeBahaId",
                table: "tblNoeKhakBardari_NoeKhakBardariEzafeBaha",
                column: "NoeKhakBardariEzafeBahaId");

            migrationBuilder.CreateIndex(
                name: "IX_tblNoeKhakBardari_NoeKhakBardariEzafeBaha_NoeKhakBardariId",
                table: "tblNoeKhakBardari_NoeKhakBardariEzafeBaha",
                column: "NoeKhakBardariId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblNoeKhakBardari_NoeKhakBardariEzafeBaha");
        }
    }
}
