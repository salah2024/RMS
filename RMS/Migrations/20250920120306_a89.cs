using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a89 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblKhakRiziBarAvord",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoeRah = table.Column<int>(type: "int", nullable: false),
                    NoeDaneBandi = table.Column<int>(type: "int", nullable: false),
                    NoeHajmKhakRizi = table.Column<int>(type: "int", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblKhakRiziBarAvord", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblKhakRiziBarAvord_tblBaravordUser_BarAvordId",
                        column: x => x.BarAvordId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblKhakRiziDarsad",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoeRah = table.Column<int>(type: "int", nullable: false),
                    NoeDaneBandi = table.Column<int>(type: "int", nullable: false),
                    NoeHajmKhakRizi = table.Column<int>(type: "int", nullable: false),
                    Darsad = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblKhakRiziDarsad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblKhakRiziItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemFBShomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblKhakRiziItem", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblKhakRiziBarAvord_BarAvordId",
                table: "tblKhakRiziBarAvord",
                column: "BarAvordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblKhakRiziBarAvord");

            migrationBuilder.DropTable(
                name: "tblKhakRiziDarsad");

            migrationBuilder.DropTable(
                name: "tblKhakRiziItem");
        }
    }
}
