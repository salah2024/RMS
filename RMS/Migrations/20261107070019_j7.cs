using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class j7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromKM",
                schema: "dbo",
                table: "tblPayKaniInfoForBarAvord",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "KMNum",
                schema: "dbo",
                table: "tblPayKaniInfoForBarAvord",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ToKM",
                schema: "dbo",
                table: "tblPayKaniInfoForBarAvord",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NoeFBId",
                schema: "dbo",
                table: "tblAmalyateKhakiInfoForBarAvord",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromKM",
                schema: "dbo",
                table: "tblPayKaniInfoForBarAvord");

            migrationBuilder.DropColumn(
                name: "KMNum",
                schema: "dbo",
                table: "tblPayKaniInfoForBarAvord");

            migrationBuilder.DropColumn(
                name: "ToKM",
                schema: "dbo",
                table: "tblPayKaniInfoForBarAvord");

            migrationBuilder.DropColumn(
                name: "NoeFBId",
                schema: "dbo",
                table: "tblAmalyateKhakiInfoForBarAvord");
        }
    }
}
