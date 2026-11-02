using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class g19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tblZarayebFosoul",
                table: "tblZarayebFosoul");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "tblZarayebFosoul",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblZarayebFosoul",
                table: "tblZarayebFosoul",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_tblZarayebFosoul_FaslId",
                table: "tblZarayebFosoul",
                column: "FaslId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tblZarayebFosoul",
                table: "tblZarayebFosoul");

            migrationBuilder.DropIndex(
                name: "IX_tblZarayebFosoul_FaslId",
                table: "tblZarayebFosoul");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "tblZarayebFosoul");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblZarayebFosoul",
                table: "tblZarayebFosoul",
                column: "FaslId");
        }
    }
}
