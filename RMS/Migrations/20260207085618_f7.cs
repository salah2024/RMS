using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class f7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoeFB",
                table: "tblNiroMasalehMashin",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VahedId",
                table: "tblNiroMasalehMashin",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "tblNiroMasalehMashin",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tblNiroMasalehMashin_VahedId",
                table: "tblNiroMasalehMashin",
                column: "VahedId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblNiroMasalehMashin_tblVahed_VahedId",
                table: "tblNiroMasalehMashin",
                column: "VahedId",
                principalTable: "tblVahed",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblNiroMasalehMashin_tblVahed_VahedId",
                table: "tblNiroMasalehMashin");

            migrationBuilder.DropIndex(
                name: "IX_tblNiroMasalehMashin_VahedId",
                table: "tblNiroMasalehMashin");

            migrationBuilder.DropColumn(
                name: "NoeFB",
                table: "tblNiroMasalehMashin");

            migrationBuilder.DropColumn(
                name: "VahedId",
                table: "tblNiroMasalehMashin");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "tblNiroMasalehMashin");
        }
    }
}
