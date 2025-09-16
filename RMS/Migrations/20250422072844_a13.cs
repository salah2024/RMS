using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
        name: "PK_tblOperation",
        table: "tblOperation");

            // اضافه کردن مجدد کلید اصلی روی Id
            migrationBuilder.AddPrimaryKey(
                name: "PK_tblOperation",
                table: "tblOperation",
                column: "Id");

           

            migrationBuilder.CreateIndex(
                name: "IX_tblOperation_Id_Year",
                table: "tblOperation",
                columns: new[] { "Id", "Year" },
                unique: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblOperation_tblOperation_ParentId",
                table: "tblOperation");

            migrationBuilder.DropIndex(
                name: "IX_tblOperation_Id_Year",
                table: "tblOperation");

            migrationBuilder.AddForeignKey(
                name: "FK_tblOperation_tblOperation_ParentId",
                table: "tblOperation",
                column: "ParentId",
                principalTable: "tblOperation",
                principalColumn: "Id");
        }
    }
}
