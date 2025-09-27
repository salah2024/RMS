using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a93 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblEzafeBahaKhakRizi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConditionContextId = table.Column<long>(type: "bigint", nullable: false),
                    Year = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblEzafeBahaKhakRizi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblEzafeBahaKhakRizi_tblConditionContext_ConditionContextId",
                        column: x => x.ConditionContextId,
                        principalTable: "tblConditionContext",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblEzafeBahaKhakRiziAddItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemFBShomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EzafeBahaKhakRiziId = table.Column<long>(type: "bigint", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalWorking = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblEzafeBahaKhakRiziAddItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblEzafeBahaKhakRiziAddItems_tblEzafeBahaKhakRizi_EzafeBahaKhakRiziId",
                        column: x => x.EzafeBahaKhakRiziId,
                        principalTable: "tblEzafeBahaKhakRizi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblEzafeBahaKhakRizi_ConditionContextId",
                table: "tblEzafeBahaKhakRizi",
                column: "ConditionContextId");

            migrationBuilder.CreateIndex(
                name: "IX_tblEzafeBahaKhakRiziAddItems_EzafeBahaKhakRiziId",
                table: "tblEzafeBahaKhakRiziAddItems",
                column: "EzafeBahaKhakRiziId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblEzafeBahaKhakRiziAddItems");

            migrationBuilder.DropTable(
                name: "tblEzafeBahaKhakRizi");
        }
    }
}
