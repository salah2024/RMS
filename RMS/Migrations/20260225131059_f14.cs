using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class f14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblMenuItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMenuItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblMenuItem_tblMenuItem_ParentId",
                        column: x => x.ParentId,
                        principalTable: "tblMenuItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblPermission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Group = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPermission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblMenuPermission",
                columns: table => new
                {
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMenuPermission", x => new { x.MenuItemId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_tblMenuPermission_tblMenuItem_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "tblMenuItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblMenuPermission_tblPermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "tblPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblRolePermission",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRolePermission", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_tblRolePermission_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblRolePermission_tblPermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "tblPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblMenuItem_ParentId",
                table: "tblMenuItem",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_tblMenuPermission_PermissionId",
                table: "tblMenuPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPermission_Key",
                table: "tblPermission",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblRolePermission_PermissionId",
                table: "tblRolePermission",
                column: "PermissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblMenuPermission");

            migrationBuilder.DropTable(
                name: "tblRolePermission");

            migrationBuilder.DropTable(
                name: "tblMenuItem");

            migrationBuilder.DropTable(
                name: "tblPermission");
        }
    }
}
