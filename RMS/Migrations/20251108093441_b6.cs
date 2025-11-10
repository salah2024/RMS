using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class b6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckData",
                table: "tblOperation");

            migrationBuilder.DropColumn(
                name: "HasEnteringValue",
                table: "tblOperation");

            migrationBuilder.DropColumn(
                name: "MaxValue",
                table: "tblOperation");

            migrationBuilder.DropColumn(
                name: "MinValue",
                table: "tblOperation");

            migrationBuilder.CreateTable(
                name: "tblOperationDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckData = table.Column<bool>(type: "bit", nullable: true),
                    HasEnteringValue = table.Column<bool>(type: "bit", nullable: true),
                    MinValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxMinValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UseMinForInsert = table.Column<bool>(type: "bit", nullable: false),
                    OperationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOperationDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblOperationDetail_tblOperation_OperationId",
                        column: x => x.OperationId,
                        principalTable: "tblOperation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblOperationDetail_OperationId",
                table: "tblOperationDetail",
                column: "OperationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblOperationDetail");

            migrationBuilder.AddColumn<bool>(
                name: "CheckData",
                table: "tblOperation",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasEnteringValue",
                table: "tblOperation",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MaxValue",
                table: "tblOperation",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinValue",
                table: "tblOperation",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
