using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class d3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblRizMetreUsersHistory",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RizMetreUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Shomareh = table.Column<long>(type: "bigint", nullable: false),
                    ShomarehNew = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sharh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tedad = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Tool = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Arz = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Ertefa = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Vazn = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    MeghdarJoz = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Des = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRizMetreUsersHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblRizMetreUsersHistory_tblRizMetreUsers_RizMetreUsersId",
                        column: x => x.RizMetreUsersId,
                        principalTable: "tblRizMetreUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblRizMetreUsersHistory_RizMetreUsersId",
                table: "tblRizMetreUsersHistory",
                column: "RizMetreUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblRizMetreUsersHistory");
        }
    }
}
