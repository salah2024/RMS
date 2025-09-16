using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a77 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShomarehNew",
                table: "tblRizMetreUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GetExistingKMAmalyateKhakiInfoWithBarAvordDto",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaravordUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FromKM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToKM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KMNum = table.Column<int>(type: "int", nullable: false),
                    FromKMSplit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToKMSplit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GetExistingKMAmalyateKhakiInfoWithBarAvordDto");

            migrationBuilder.DropColumn(
                name: "ShomarehNew",
                table: "tblRizMetreUsers");
        }
    }
}
