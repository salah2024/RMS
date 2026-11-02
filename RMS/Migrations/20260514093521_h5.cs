using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class h5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tblZaribPishnahadiPaymanKarFosoul_ZaribPishnahadiPaymanKarId",
                table: "tblZaribPishnahadiPaymanKarFosoul");

            migrationBuilder.CreateIndex(
                name: "IX_tblZaribPishnahadiPaymanKarFosoul_ZaribPishnahadiPaymanKarId",
                table: "tblZaribPishnahadiPaymanKarFosoul",
                column: "ZaribPishnahadiPaymanKarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tblZaribPishnahadiPaymanKarFosoul_ZaribPishnahadiPaymanKarId",
                table: "tblZaribPishnahadiPaymanKarFosoul");

            migrationBuilder.CreateIndex(
                name: "IX_tblZaribPishnahadiPaymanKarFosoul_ZaribPishnahadiPaymanKarId",
                table: "tblZaribPishnahadiPaymanKarFosoul",
                column: "ZaribPishnahadiPaymanKarId",
                unique: true);
        }
    }
}
