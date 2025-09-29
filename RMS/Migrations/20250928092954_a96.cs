using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a96 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ItemFBShomareh",
                table: "tblEzafeBahaKhakRiziAddItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "tblKhakRiziEzafeBahaBarAvord",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EzafeBahaKhakRiziId = table.Column<long>(type: "bigint", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblKhakRiziEzafeBahaBarAvord", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblKhakRiziEzafeBahaBarAvord_tblBaravordUser_BarAvordId",
                        column: x => x.BarAvordId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblKhakRiziEzafeBahaBarAvord_tblEzafeBahaKhakRizi_EzafeBahaKhakRiziId",
                        column: x => x.EzafeBahaKhakRiziId,
                        principalTable: "tblEzafeBahaKhakRizi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblKhakRiziEzafeBahaBarAvord_BarAvordId",
                table: "tblKhakRiziEzafeBahaBarAvord",
                column: "BarAvordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblKhakRiziEzafeBahaBarAvord_EzafeBahaKhakRiziId",
                table: "tblKhakRiziEzafeBahaBarAvord",
                column: "EzafeBahaKhakRiziId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblKhakRiziEzafeBahaBarAvord");

            migrationBuilder.AlterColumn<string>(
                name: "ItemFBShomareh",
                table: "tblEzafeBahaKhakRiziAddItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
