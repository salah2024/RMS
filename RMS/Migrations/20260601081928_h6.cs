using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class h6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblTadilIndexFosoul",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    NoeFB = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseType = table.Column<int>(type: "int", nullable: false),
                    CourseCode = table.Column<int>(type: "int", nullable: false),
                    IndexValue = table.Column<decimal>(type: "decimal(18,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTadilIndexFosoul", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblTadilIndexNoeFB",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    NoeFB = table.Column<int>(type: "int", nullable: false),
                    CourseType = table.Column<int>(type: "int", nullable: false),
                    CourseCode = table.Column<int>(type: "int", nullable: false),
                    IndexValue = table.Column<decimal>(type: "decimal(18,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTadilIndexNoeFB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblUserNotification",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelateLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserNotification", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblUserNotification_tblBaravordUser_BarAvordId",
                        column: x => x.BarAvordId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblUserNotification_BarAvordId",
                table: "tblUserNotification",
                column: "BarAvordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblTadilIndexFosoul");

            migrationBuilder.DropTable(
                name: "tblTadilIndexNoeFB");

            migrationBuilder.DropTable(
                name: "tblUserNotification");
        }
    }
}
