using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class d5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromKM",
                table: "tblPayKaniInfoForBarAvord");

            migrationBuilder.DropColumn(
                name: "ToKM",
                table: "tblPayKaniInfoForBarAvord");

            migrationBuilder.RenameColumn(
                name: "KMNum",
                table: "tblPayKaniInfoForBarAvord",
                newName: "NoeFBId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NoeFBId",
                table: "tblPayKaniInfoForBarAvord",
                newName: "KMNum");

            migrationBuilder.AddColumn<string>(
                name: "FromKM",
                table: "tblPayKaniInfoForBarAvord",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToKM",
                table: "tblPayKaniInfoForBarAvord",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
