using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class g8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblKhakRiziDarsad");

            migrationBuilder.CreateTable(
                name: "tblKhakRiziCondition",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoeRah = table.Column<int>(type: "int", nullable: false),
                    NoeDaneBandi = table.Column<int>(type: "int", nullable: false),
                    NoeHajmKhakRizi = table.Column<int>(type: "int", nullable: false),
                    FBShomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalWork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CharacterPlus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoeFB = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblKhakRiziCondition", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblKhakRiziCondition");

            migrationBuilder.CreateTable(
                name: "tblKhakRiziDarsad",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Darsad = table.Column<int>(type: "int", nullable: false),
                    NoeDaneBandi = table.Column<int>(type: "int", nullable: false),
                    NoeHajmKhakRizi = table.Column<int>(type: "int", nullable: false),
                    NoeRah = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblKhakRiziDarsad", x => x.Id);
                });
        }
    }
}
