using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class f6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblGeneralProjectTiming_tblProject_ProjectId",
                table: "tblGeneralProjectTiming");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "tblGeneralProjectTiming",
                newName: "BaravordUserId");

            migrationBuilder.RenameIndex(
                name: "IX_tblGeneralProjectTiming_ProjectId",
                table: "tblGeneralProjectTiming",
                newName: "IX_tblGeneralProjectTiming_BaravordUserId");

            migrationBuilder.CreateTable(
                name: "GetOperationNameHasValueDto",
                columns: table => new
                {
                    OperationId = table.Column<long>(type: "bigint", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: false),
                    OperationLatinName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxMeghdarMeghias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxCodeAmelMoaser = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "OperationNameAnalizBahaWithOperationDto",
                columns: table => new
                {
                    OperationExceptOpNodeLatinName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeAmel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoeMashinAlatName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MashinTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeghdarMeghias = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ZaribKarKard = table.Column<int>(type: "int", nullable: false),
                    IsAmelMoaser = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.AddForeignKey(
                name: "FK_tblGeneralProjectTiming_tblBaravordUser_BaravordUserId",
                table: "tblGeneralProjectTiming",
                column: "BaravordUserId",
                principalTable: "tblBaravordUser",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblGeneralProjectTiming_tblBaravordUser_BaravordUserId",
                table: "tblGeneralProjectTiming");

            migrationBuilder.DropTable(
                name: "GetOperationNameHasValueDto");

            migrationBuilder.DropTable(
                name: "OperationNameAnalizBahaWithOperationDto");

            migrationBuilder.RenameColumn(
                name: "BaravordUserId",
                table: "tblGeneralProjectTiming",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_tblGeneralProjectTiming_BaravordUserId",
                table: "tblGeneralProjectTiming",
                newName: "IX_tblGeneralProjectTiming_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblGeneralProjectTiming_tblProject_ProjectId",
                table: "tblGeneralProjectTiming",
                column: "ProjectId",
                principalTable: "tblProject",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
