using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a91 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoeHajmKhakRizi",
                table: "tblKhakRiziBarAvord");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "tblKhakRiziBarAvord");

            migrationBuilder.AddColumn<string>(
                name: "NoeHajmKhakRizi_Value",
                table: "tblKhakRiziBarAvord",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoeHajmKhakRizi_Value",
                table: "tblKhakRiziBarAvord");

            migrationBuilder.AddColumn<int>(
                name: "NoeHajmKhakRizi",
                table: "tblKhakRiziBarAvord",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "tblKhakRiziBarAvord",
                type: "decimal(18,4)",
                nullable: true);
        }
    }
}
