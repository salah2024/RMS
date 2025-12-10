using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class c17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblFosoul_tblNoeFosoul_NoeFosoulId",
                table: "tblFosoul");

            migrationBuilder.DropTable(
                name: "tblNoeFosoul");

            migrationBuilder.DropIndex(
                name: "IX_tblFosoul_NoeFosoulId",
                table: "tblFosoul");

            migrationBuilder.DropColumn(
                name: "NoeFosoulId",
                table: "tblFosoul");

            migrationBuilder.DropColumn(
                name: "NoeFB",
                table: "tblBaravordUser");

            migrationBuilder.AddColumn<string>(
                name: "NoeFBs",
                table: "tblBaravordUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoeFBs",
                table: "tblBaravordUser");

            migrationBuilder.AddColumn<long>(
                name: "NoeFosoulId",
                table: "tblFosoul",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "NoeFB",
                table: "tblBaravordUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "tblNoeFosoul",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoeFaslName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblNoeFosoul", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblFosoul_NoeFosoulId",
                table: "tblFosoul",
                column: "NoeFosoulId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblFosoul_tblNoeFosoul_NoeFosoulId",
                table: "tblFosoul",
                column: "NoeFosoulId",
                principalTable: "tblNoeFosoul",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
