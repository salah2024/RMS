using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class c5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblOstan",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOstan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblShahr",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OstanId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblShahr", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblShahr_tblOstan_OstanId",
                        column: x => x.OstanId,
                        principalTable: "tblOstan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblBakhsh",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShahrId = table.Column<long>(type: "bigint", nullable: false),
                    Zarib = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBakhsh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBakhsh_tblShahr_ShahrId",
                        column: x => x.ShahrId,
                        principalTable: "tblShahr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblBakhsh_ShahrId",
                table: "tblBakhsh",
                column: "ShahrId");

            migrationBuilder.CreateIndex(
                name: "IX_tblShahr_OstanId",
                table: "tblShahr",
                column: "OstanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblBakhsh");

            migrationBuilder.DropTable(
                name: "tblShahr");

            migrationBuilder.DropTable(
                name: "tblOstan");
        }
    }
}
