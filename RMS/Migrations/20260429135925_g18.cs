using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class g18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tblZarayebFosoul",
                table: "tblZarayebFosoul");

            migrationBuilder.DropIndex(
                name: "IX_tblZarayebFosoul_FaslId",
                table: "tblZarayebFosoul");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "tblZarayebFosoul");

            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "tblZarayebFosoul");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "tblZarayebFosoul");

            migrationBuilder.DropColumn(
                name: "RemoveDateTime",
                table: "tblZarayebFosoul");

            migrationBuilder.DropColumn(
                name: "UserInserter",
                table: "tblZarayebFosoul");

            migrationBuilder.DropColumn(
                name: "UserRemover",
                table: "tblZarayebFosoul");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblZarayebFosoul",
                table: "tblZarayebFosoul",
                column: "FaslId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tblZarayebFosoul",
                table: "tblZarayebFosoul");

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "tblZarayebFosoul",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDateTime",
                table: "tblZarayebFosoul",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "tblZarayebFosoul",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveDateTime",
                table: "tblZarayebFosoul",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserInserter",
                table: "tblZarayebFosoul",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserRemover",
                table: "tblZarayebFosoul",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblZarayebFosoul",
                table: "tblZarayebFosoul",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_tblZarayebFosoul_FaslId",
                table: "tblZarayebFosoul",
                column: "FaslId");
        }
    }
}
