using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class g15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CharacterPlus",
                table: "tblKhakRiziCondition");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "tblBakhsh");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CharacterPlus",
                table: "tblKhakRiziCondition",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "tblBakhsh",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
