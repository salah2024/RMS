using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class h9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "tblTadilIndexFosoul");

            migrationBuilder.DropColumn(
                name: "NoeFB",
                table: "tblTadilIndexFosoul");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "tblTadilIndexFosoul");

            migrationBuilder.AddColumn<long>(
                name: "FosoulId",
                table: "tblTadilIndexFosoul",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_tblTadilIndexFosoul_FosoulId",
                table: "tblTadilIndexFosoul",
                column: "FosoulId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblTadilIndexFosoul_tblFosoul_FosoulId",
                table: "tblTadilIndexFosoul",
                column: "FosoulId",
                principalTable: "tblFosoul",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblTadilIndexFosoul_tblFosoul_FosoulId",
                table: "tblTadilIndexFosoul");

            migrationBuilder.DropIndex(
                name: "IX_tblTadilIndexFosoul_FosoulId",
                table: "tblTadilIndexFosoul");

            migrationBuilder.DropColumn(
                name: "FosoulId",
                table: "tblTadilIndexFosoul");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "tblTadilIndexFosoul",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NoeFB",
                table: "tblTadilIndexFosoul",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "tblTadilIndexFosoul",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
