using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class f11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ManagementName",
                table: "tblCompany",
                newName: "MobileNumber");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "tblCompany",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FaxNumber",
                table: "tblCompany",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagementFirstName",
                table: "tblCompany",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagementLastName",
                table: "tblCompany",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "tblCompany");

            migrationBuilder.DropColumn(
                name: "FaxNumber",
                table: "tblCompany");

            migrationBuilder.DropColumn(
                name: "ManagementFirstName",
                table: "tblCompany");

            migrationBuilder.DropColumn(
                name: "ManagementLastName",
                table: "tblCompany");

            migrationBuilder.RenameColumn(
                name: "MobileNumber",
                table: "tblCompany",
                newName: "ManagementName");
        }
    }
}
