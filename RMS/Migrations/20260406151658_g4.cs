using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class g4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EnableDeleting",
                table: "tblOperation_ItemsFB",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EnableEditing",
                table: "tblOperation_ItemsFB",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnableDeleting",
                table: "tblOperation_ItemsFB");

            migrationBuilder.DropColumn(
                name: "EnableEditing",
                table: "tblOperation_ItemsFB");
        }
    }
}
