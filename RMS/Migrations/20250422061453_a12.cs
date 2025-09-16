using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "tblOperation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ItemsFBDependQuestionForAbnieFaniForSPDto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    QuesForAbnieFaniId = table.Column<long>(type: "bigint", nullable: false),
                    ItemShomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultValue = table.Column<int>(type: "int", nullable: false),
                    Vahed = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ItemsFBShomarehValueShomarehUpdateProcedureDto",
                columns: table => new
                {
                    check = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemsFBDependQuestionForAbnieFaniForSPDto");

            migrationBuilder.DropTable(
                name: "ItemsFBShomarehValueShomarehUpdateProcedureDto");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "tblOperation");
        }
    }
}
