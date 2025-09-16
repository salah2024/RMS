using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a75 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "NoeKhakBardariId",
                table: "tblAmalyateKhakiInfoForBarAvordDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "tblNoeKhakBardari",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FBItemShomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblNoeKhakBardari", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblAmalyateKhakiInfoForBarAvordDetails_NoeKhakBardariId",
                table: "tblAmalyateKhakiInfoForBarAvordDetails",
                column: "NoeKhakBardariId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblAmalyateKhakiInfoForBarAvordDetails_tblNoeKhakBardari_NoeKhakBardariId",
                table: "tblAmalyateKhakiInfoForBarAvordDetails",
                column: "NoeKhakBardariId",
                principalTable: "tblNoeKhakBardari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblAmalyateKhakiInfoForBarAvordDetails_tblNoeKhakBardari_NoeKhakBardariId",
                table: "tblAmalyateKhakiInfoForBarAvordDetails");

            migrationBuilder.DropTable(
                name: "tblNoeKhakBardari");

            migrationBuilder.DropIndex(
                name: "IX_tblAmalyateKhakiInfoForBarAvordDetails_NoeKhakBardariId",
                table: "tblAmalyateKhakiInfoForBarAvordDetails");

            migrationBuilder.DropColumn(
                name: "NoeKhakBardariId",
                table: "tblAmalyateKhakiInfoForBarAvordDetails");
        }
    }
}
