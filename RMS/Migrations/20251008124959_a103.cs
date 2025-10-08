using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a103 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "GetExistingKMPayKaniInfoWithBarAvordDto");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "tblPayKaniInfoForBarAvord",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "tblPayKaniInfoForBarAvord");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "GetExistingKMPayKaniInfoWithBarAvordDto",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
