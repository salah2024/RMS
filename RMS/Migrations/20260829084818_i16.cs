using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class i16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FBShomarehMain",
                table: "tblFehrestBahaItemsReplace",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_FehrestBahaItemsReplaces_FBShomarehMain",
                table: "tblFehrestBahaItemsReplace",
                column: "FBShomarehMain");

            migrationBuilder.CreateIndex(
                name: "IX_FehrestBahas_Sal_NoeFB",
                table: "tblFehrestBaha",
                columns: new[] { "Sal", "NoeFB" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FehrestBahaItemsReplaces_FBShomarehMain",
                table: "tblFehrestBahaItemsReplace");

            migrationBuilder.DropIndex(
                name: "IX_FehrestBahas_Sal_NoeFB",
                table: "tblFehrestBaha");

            migrationBuilder.AlterColumn<string>(
                name: "FBShomarehMain",
                table: "tblFehrestBahaItemsReplace",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
