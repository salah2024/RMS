using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblOperationHasAddedOperationsLevelNumber",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationHasAddedOperationsId = table.Column<long>(type: "bigint", nullable: false),
                    LevelNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOperationHasAddedOperationsLevelNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblOperationHasAddedOperationsLevelNumber_tblOperationHasAddedOperations_OperationHasAddedOperationsId",
                        column: x => x.OperationHasAddedOperationsId,
                        principalTable: "tblOperationHasAddedOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblOperationHasAddedOperationsLevelNumber_OperationHasAddedOperationsId",
                table: "tblOperationHasAddedOperationsLevelNumber",
                column: "OperationHasAddedOperationsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblOperationHasAddedOperationsLevelNumber");
        }
    }
}
