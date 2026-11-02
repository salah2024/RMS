using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class f10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CEOName",
                table: "tblUserDetail");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "tblUserDetail");

            migrationBuilder.DropColumn(
                name: "userType",
                table: "tblUserDetail");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "tblUserDetail",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "tblCompany",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyNationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagementName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyType = table.Column<int>(type: "int", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCompany", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblCompanyManagementHistory",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCompanyManagementHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblCompanyManagementHistory_tblCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tblCompany",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblUserDetail_CompanyId",
                table: "tblUserDetail",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCompanyManagementHistory_CompanyId",
                table: "tblCompanyManagementHistory",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblUserDetail_tblCompany_CompanyId",
                table: "tblUserDetail",
                column: "CompanyId",
                principalTable: "tblCompany",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblUserDetail_tblCompany_CompanyId",
                table: "tblUserDetail");

            migrationBuilder.DropTable(
                name: "tblCompanyManagementHistory");

            migrationBuilder.DropTable(
                name: "tblCompany");

            migrationBuilder.DropIndex(
                name: "IX_tblUserDetail_CompanyId",
                table: "tblUserDetail");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "tblUserDetail");

            migrationBuilder.AddColumn<string>(
                name: "CEOName",
                table: "tblUserDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "tblUserDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "userType",
                table: "tblUserDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
