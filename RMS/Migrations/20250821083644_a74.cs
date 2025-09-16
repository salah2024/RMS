using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a74 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NoeKhakBardariId",
                table: "tblAmalyateKhakiInfoForBarAvordDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "tblNoeKhakBardari",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FBItemShomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblNoeKhakBardari", x => x.ID);
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
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
