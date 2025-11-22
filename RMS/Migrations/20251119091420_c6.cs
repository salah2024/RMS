using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class c6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Zarib",
                table: "tblBakhsh",
                newName: "ZaribToziAb");

            migrationBuilder.AddColumn<decimal>(
                name: "ZaribAbKhizDari",
                table: "tblBakhsh",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ZaribAbRostaii",
                table: "tblBakhsh",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ZaribAbiariVaZeh",
                table: "tblBakhsh",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ZaribAbnie",
                table: "tblBakhsh",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ZaribBargh",
                table: "tblBakhsh",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ZaribChah",
                table: "tblBakhsh",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ZaribEnteghalAb",
                table: "tblBakhsh",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ZaribEnteghalFazelAb",
                table: "tblBakhsh",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ZaribGhanat",
                table: "tblBakhsh",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ZaribMechanic",
                table: "tblBakhsh",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ZaribRah",
                table: "tblBakhsh",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ZaribRahDari",
                table: "tblBakhsh",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ZaribSad",
                table: "tblBakhsh",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ZaribTahteFeshar",
                table: "tblBakhsh",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZaribAbKhizDari",
                table: "tblBakhsh");

            migrationBuilder.DropColumn(
                name: "ZaribAbRostaii",
                table: "tblBakhsh");

            migrationBuilder.DropColumn(
                name: "ZaribAbiariVaZeh",
                table: "tblBakhsh");

            migrationBuilder.DropColumn(
                name: "ZaribAbnie",
                table: "tblBakhsh");

            migrationBuilder.DropColumn(
                name: "ZaribBargh",
                table: "tblBakhsh");

            migrationBuilder.DropColumn(
                name: "ZaribChah",
                table: "tblBakhsh");

            migrationBuilder.DropColumn(
                name: "ZaribEnteghalAb",
                table: "tblBakhsh");

            migrationBuilder.DropColumn(
                name: "ZaribEnteghalFazelAb",
                table: "tblBakhsh");

            migrationBuilder.DropColumn(
                name: "ZaribGhanat",
                table: "tblBakhsh");

            migrationBuilder.DropColumn(
                name: "ZaribMechanic",
                table: "tblBakhsh");

            migrationBuilder.DropColumn(
                name: "ZaribRah",
                table: "tblBakhsh");

            migrationBuilder.DropColumn(
                name: "ZaribRahDari",
                table: "tblBakhsh");

            migrationBuilder.DropColumn(
                name: "ZaribSad",
                table: "tblBakhsh");

            migrationBuilder.DropColumn(
                name: "ZaribTahteFeshar",
                table: "tblBakhsh");

            migrationBuilder.RenameColumn(
                name: "ZaribToziAb",
                table: "tblBakhsh",
                newName: "Zarib");
        }
    }
}
