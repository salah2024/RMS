using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class a1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblAbadDivarBali",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    h = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    x = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    b = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    f = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    m = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAbadDivarBali", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblAnalizBaha",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FBShomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoeFB = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    NiroMasalehMashinId = table.Column<long>(type: "bigint", nullable: false),
                    Vahed = table.Column<long>(type: "bigint", nullable: false),
                    MeghdarMeghias = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BahayeVahed = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    bahayeKol = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAnalizBaha", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblBaravordUser",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Num = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    NoeFB = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBaravordUser", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblBaseInfoType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LatinName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBaseInfoType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblConditionGroup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConditionGroupName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblConditionGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblFehrestBaha",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BahayeVahed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vahed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sharh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Shomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sal = table.Column<long>(type: "bigint", nullable: false),
                    NoeFB = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFehrestBaha", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblHadAksarErtefaKoole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HadAksarErtefaKoole = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TedadDahaneh = table.Column<int>(type: "int", nullable: false),
                    DahaneAbro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblHadAksarErtefaKoole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblItemsFields",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemShomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldType = table.Column<int>(type: "int", nullable: false),
                    Vahed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEnteringValue = table.Column<bool>(type: "bit", nullable: false),
                    DefaultValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoeFB = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItemsFields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblItemsForGetValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemShomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemShomarehForGetValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    RizMetreFieldsRequire = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItemsForGetValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblItemsHasCondition",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemFBShomareh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItemsHasCondition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblItemsHasHaml",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemsFB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    NoeFB = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItemsHasHaml", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblMashinType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MashinTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LatinName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMashinType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblNiroMasalehMashin",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeAmel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblNiroMasalehMashin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOperation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LatinName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    FunctionCall = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOperation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblOperation_tblOperation_ParentId",
                        column: x => x.ParentId,
                        principalTable: "tblOperation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tblOperationHasAddedOperationsType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LatinName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOperationHasAddedOperationsType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOperationsOfHaml",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOperationsOfHaml", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblProject",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProject", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblQuesForAbnieFani",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasGetValue = table.Column<bool>(type: "bit", nullable: false),
                    DefaultValue = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    NoeFB = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsEzafeBaha = table.Column<bool>(type: "bit", nullable: false),
                    ObjectType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblQuesForAbnieFani", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblZaribRoadType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZaribSheni = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ZaribSakhtehNashodeh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    NoeFB = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblZaribRoadType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblAmalyateKhakiInfoForBarAvord",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaravordUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FromKM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToKM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KMNum = table.Column<int>(type: "int", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAmalyateKhakiInfoForBarAvord", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblAmalyateKhakiInfoForBarAvord_tblBaravordUser_BaravordUserId",
                        column: x => x.BaravordUserId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblItemsFBShomarehValueShomareh",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FBShomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GetValuesShomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItemsFBShomarehValueShomareh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblItemsFBShomarehValueShomareh_tblBaravordUser_BarAvordId",
                        column: x => x.BarAvordId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblPolVaAbroBarAvord",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TedadDahaneh = table.Column<int>(type: "int", nullable: false),
                    DahaneAbro = table.Column<double>(type: "float", nullable: false),
                    HadAksarErtefaKoole = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Hs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZavieBie = table.Column<int>(type: "int", nullable: false),
                    ToolAbro = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    X = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Y = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NoeBanaii = table.Column<short>(type: "smallint", nullable: false),
                    NahveEjraDal = table.Column<short>(type: "smallint", nullable: false),
                    PolNum = table.Column<long>(type: "bigint", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPolVaAbroBarAvord", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblPolVaAbroBarAvord_tblBaravordUser_BarAvordId",
                        column: x => x.BarAvordId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblBaseInfo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LatinName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    Priority = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBaseInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBaseInfo_tblBaseInfoType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "tblBaseInfoType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblConditionContext",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConditionGroupId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblConditionContext", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblConditionContext_tblConditionGroup_ConditionGroupId",
                        column: x => x.ConditionGroupId,
                        principalTable: "tblConditionGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblAbadeKoole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HadAksarErtefaKooleId = table.Column<long>(type: "bigint", nullable: false),
                    Hs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    a1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    a2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    b1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    b2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    c1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    c2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    f = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    m = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    t = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HadAghalZavieEstekak = table.Column<int>(type: "int", nullable: false),
                    DerzEnbesat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    p1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    p2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    e = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    n = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    k = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAbadeKoole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblAbadeKoole_tblHadAksarErtefaKoole_HadAksarErtefaKooleId",
                        column: x => x.HadAksarErtefaKooleId,
                        principalTable: "tblHadAksarErtefaKoole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblAppOperationInfoMain",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperationId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    XState = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    YState = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    DateSending = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeSending = table.Column<TimeSpan>(type: "time", nullable: true),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAppOperationInfoMain", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblAppOperationInfoMain_tblOperation_OperationId",
                        column: x => x.OperationId,
                        principalTable: "tblOperation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblAppQuestion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LatinTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAppQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblAppQuestion_tblOperation_OperationId",
                        column: x => x.OperationId,
                        principalTable: "tblOperation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblNiroZaribKarKard",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeAmel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZaribKarKard = table.Column<int>(type: "int", nullable: false),
                    IsAmelMoaser = table.Column<bool>(type: "bit", nullable: false),
                    OperationId = table.Column<long>(type: "bigint", nullable: false),
                    NoeMashinAlatId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblNiroZaribKarKard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblNiroZaribKarKard_tblMashinType_NoeMashinAlatId",
                        column: x => x.NoeMashinAlatId,
                        principalTable: "tblMashinType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblNiroZaribKarKard_tblOperation_OperationId",
                        column: x => x.OperationId,
                        principalTable: "tblOperation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblOperation_ItemsFB",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationId = table.Column<long>(type: "bigint", nullable: false),
                    ItemsFBShomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NextOperation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOperation_ItemsFB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblOperation_ItemsFB_tblOperation_OperationId",
                        column: x => x.OperationId,
                        principalTable: "tblOperation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblOperationAmelMoaser",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationId = table.Column<long>(type: "bigint", nullable: false),
                    CodeAmel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOperationAmelMoaser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblOperationAmelMoaser_tblOperation_OperationId",
                        column: x => x.OperationId,
                        principalTable: "tblOperation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblOperationHasAddedOperations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationId = table.Column<long>(type: "bigint", nullable: false),
                    AddedOperationId = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOperationHasAddedOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblOperationHasAddedOperations_tblOperation_AddedOperationId",
                        column: x => x.AddedOperationId,
                        principalTable: "tblOperation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblOperationHasAddedOperations_tblOperation_OperationId",
                        column: x => x.OperationId,
                        principalTable: "tblOperation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblKiloMetrazhOfHaml",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KM = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OperationOfHamlId = table.Column<long>(type: "bigint", nullable: false),
                    BarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblKiloMetrazhOfHaml", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblKiloMetrazhOfHaml_tblBaravordUser_BarAvordId",
                        column: x => x.BarAvordId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblKiloMetrazhOfHaml_tblOperationsOfHaml_OperationOfHamlId",
                        column: x => x.OperationOfHamlId,
                        principalTable: "tblOperationsOfHaml",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblOperationsOfHaml_ItemsHasHaml",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationsOfHamlId = table.Column<long>(type: "bigint", nullable: false),
                    ItemsHasHamlId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOperationsOfHaml_ItemsHasHaml", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblOperationsOfHaml_ItemsHasHaml_tblItemsHasHaml_ItemsHasHamlId",
                        column: x => x.ItemsHasHamlId,
                        principalTable: "tblItemsHasHaml",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblOperationsOfHaml_ItemsHasHaml_tblOperationsOfHaml_OperationsOfHamlId",
                        column: x => x.OperationsOfHamlId,
                        principalTable: "tblOperationsOfHaml",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblOperationsOfHamlAndItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationsOfHamlId = table.Column<long>(type: "bigint", nullable: false),
                    ItemsFB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    NoeFB = table.Column<int>(type: "int", nullable: false),
                    KiloMetrazh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOperationsOfHamlAndItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblOperationsOfHamlAndItems_tblOperationsOfHaml_OperationsOfHamlId",
                        column: x => x.OperationsOfHamlId,
                        principalTable: "tblOperationsOfHaml",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblRizMetreUsers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Shomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sharh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tedad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tool = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Arz = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ertefa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Vazn = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Des = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FBId = table.Column<long>(type: "bigint", nullable: false),
                    OperationsOfHamlId = table.Column<long>(type: "bigint", nullable: false),
                    ForItem = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UseItem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRizMetreUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblRizMetreUsers_tblFehrestBaha_FBId",
                        column: x => x.FBId,
                        principalTable: "tblFehrestBaha",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblRizMetreUsers_tblOperationsOfHaml_OperationsOfHamlId",
                        column: x => x.OperationsOfHamlId,
                        principalTable: "tblOperationsOfHaml",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblZarayebTabdil",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Z1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Z2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Z3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Z4 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemShomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationsOfHamlId = table.Column<long>(type: "bigint", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    NoeFB = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblZarayebTabdil", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblZarayebTabdil_tblOperationsOfHaml_OperationsOfHamlId",
                        column: x => x.OperationsOfHamlId,
                        principalTable: "tblOperationsOfHaml",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblGeneralProjectTiming",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CountDaies = table.Column<int>(type: "int", nullable: false),
                    CourseCountDaies = table.Column<int>(type: "int", nullable: false),
                    HolyCourseCountDaies = table.Column<int>(type: "int", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGeneralProjectTiming", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblGeneralProjectTiming_tblProject_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "tblProject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSegmentsFromGEODB",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SegmentIdFromGEODB = table.Column<int>(type: "int", nullable: false),
                    OperationId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSegmentsFromGEODB", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblSegmentsFromGEODB_tblOperation_OperationId",
                        column: x => x.OperationId,
                        principalTable: "tblOperation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblSegmentsFromGEODB_tblProject_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "tblProject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblItemsFBDependQuestionForAbnieFani",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuesForAbnieFaniId = table.Column<long>(type: "bigint", nullable: false),
                    ItemShomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultValue = table.Column<int>(type: "int", nullable: false),
                    Vahed = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItemsFBDependQuestionForAbnieFani", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblItemsFBDependQuestionForAbnieFani_tblQuesForAbnieFani_QuesForAbnieFaniId",
                        column: x => x.QuesForAbnieFaniId,
                        principalTable: "tblQuesForAbnieFani",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblShomarehFBForQuesForAbnieFani",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuesForAbnieFaniId = table.Column<long>(type: "bigint", nullable: false),
                    Shomareh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblShomarehFBForQuesForAbnieFani", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblShomarehFBForQuesForAbnieFani_tblQuesForAbnieFani_QuesForAbnieFaniId",
                        column: x => x.QuesForAbnieFaniId,
                        principalTable: "tblQuesForAbnieFani",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblAmalyateKhakiInfoForBarAvordDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmalyateKhakiInfoForBarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    boolValue = table.Column<bool>(type: "bit", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAmalyateKhakiInfoForBarAvordDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblAmalyateKhakiInfoForBarAvordDetails_tblAmalyateKhakiInfoForBarAvord_AmalyateKhakiInfoForBarAvordId",
                        column: x => x.AmalyateKhakiInfoForBarAvordId,
                        principalTable: "tblAmalyateKhakiInfoForBarAvord",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblAmalyateKhakiInfoForBarAvordMore",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmalyateKhakiInfoForBarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAmalyateKhakiInfoForBarAvordMore", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblAmalyateKhakiInfoForBarAvordMore_tblAmalyateKhakiInfoForBarAvord_AmalyateKhakiInfoForBarAvordId",
                        column: x => x.AmalyateKhakiInfoForBarAvordId,
                        principalTable: "tblAmalyateKhakiInfoForBarAvord",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblDastakPolInfo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolVaAbroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Shomareh = table.Column<long>(type: "bigint", nullable: false),
                    ToolW = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Zavie = table.Column<int>(type: "int", nullable: false),
                    hMin = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDastakPolInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblDastakPolInfo_tblPolVaAbroBarAvord_PolVaAbroId",
                        column: x => x.PolVaAbroId,
                        principalTable: "tblPolVaAbroBarAvord",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblFB",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Shomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BahayeVahedZarib = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFB_tblPolVaAbroBarAvord_BarAvordId",
                        column: x => x.BarAvordId,
                        principalTable: "tblPolVaAbroBarAvord",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblQuesForAbnieFaniValues",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionForAbnieFaniId = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShomarehFBSelectedId = table.Column<int>(type: "int", nullable: false),
                    PolVaAbroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblQuesForAbnieFaniValues", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblQuesForAbnieFaniValues_tblPolVaAbroBarAvord_PolVaAbroId",
                        column: x => x.PolVaAbroId,
                        principalTable: "tblPolVaAbroBarAvord",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblQuesForAbnieFaniValues_tblQuesForAbnieFani_QuestionForAbnieFaniId",
                        column: x => x.QuestionForAbnieFaniId,
                        principalTable: "tblQuesForAbnieFani",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblItemsHasCondition_ConditionContext",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemsHasConditionId = table.Column<long>(type: "bigint", nullable: false),
                    ConditionContextId = table.Column<long>(type: "bigint", nullable: false),
                    HasEnteringValue = table.Column<bool>(type: "bit", nullable: false),
                    Des = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsShow = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    MoveToRel = table.Column<bool>(type: "bit", nullable: false),
                    ViewCheckAllRecords = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItemsHasCondition_ConditionContext", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblItemsHasCondition_ConditionContext_tblConditionContext_ConditionContextId",
                        column: x => x.ConditionContextId,
                        principalTable: "tblConditionContext",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblItemsHasCondition_ConditionContext_tblItemsHasCondition_ConditionContext_ParentId",
                        column: x => x.ParentId,
                        principalTable: "tblItemsHasCondition_ConditionContext",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tblItemsHasCondition_ConditionContext_tblItemsHasCondition_ItemsHasConditionId",
                        column: x => x.ItemsHasConditionId,
                        principalTable: "tblItemsHasCondition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblAbroDaliDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AbadeKooleId = table.Column<long>(type: "bigint", nullable: false),
                    Pos = table.Column<int>(type: "int", nullable: false),
                    Ghotr = table.Column<int>(type: "int", nullable: false),
                    Tedad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fasele = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tool = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VazMilgard1M = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VazMilgardSE = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAbroDaliDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblAbroDaliDetails_tblAbadeKoole_AbadeKooleId",
                        column: x => x.AbadeKooleId,
                        principalTable: "tblAbadeKoole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblAppOperationInfoDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppOperationInfoMainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuetionId = table.Column<long>(type: "bigint", nullable: false),
                    Answer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAppOperationInfoDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblAppOperationInfoDetails_tblAppOperationInfoMain_AppOperationInfoMainId",
                        column: x => x.AppOperationInfoMainId,
                        principalTable: "tblAppOperationInfoMain",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblAppOperationInfoDetails_tblAppQuestion_QuetionId",
                        column: x => x.QuetionId,
                        principalTable: "tblAppQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblRizKiloMetrazhOfHaml",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KiloMetrazhOfHamlId = table.Column<long>(type: "bigint", nullable: false),
                    Asfalt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sheni = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SakhteNashodeh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemFB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRizKiloMetrazhOfHaml", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblRizKiloMetrazhOfHaml_tblKiloMetrazhOfHaml_KiloMetrazhOfHamlId",
                        column: x => x.KiloMetrazhOfHamlId,
                        principalTable: "tblKiloMetrazhOfHaml",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblProjectCalendar",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeneralProjectTimingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    ActiveDeActive = table.Column<bool>(type: "bit", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProjectCalendar", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblProjectCalendar_tblGeneralProjectTiming_GeneralProjectTimingId",
                        column: x => x.GeneralProjectTimingId,
                        principalTable: "tblGeneralProjectTiming",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblAmalyateKhakiInfoForBarAvordDetailsEzafeBaha",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmalyateKhakiInfoForBarAvordDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<bool>(type: "bit", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAmalyateKhakiInfoForBarAvordDetailsEzafeBaha", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblAmalyateKhakiInfoForBarAvordDetailsEzafeBaha_tblAmalyateKhakiInfoForBarAvordDetails_AmalyateKhakiInfoForBarAvordDetailsId",
                        column: x => x.AmalyateKhakiInfoForBarAvordDetailsId,
                        principalTable: "tblAmalyateKhakiInfoForBarAvordDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblAmalyateKhakiInfoForBarAvordDetailsMore",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmalyateKhakiInfoForBarAvordDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInserter = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRemover = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAmalyateKhakiInfoForBarAvordDetailsMore", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblAmalyateKhakiInfoForBarAvordDetailsMore_tblAmalyateKhakiInfoForBarAvordDetails_AmalyateKhakiInfoForBarAvordDetailsId",
                        column: x => x.AmalyateKhakiInfoForBarAvordDetailsId,
                        principalTable: "tblAmalyateKhakiInfoForBarAvordDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblItemsAddingToFB",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemsHasCondition_ConditionContextId = table.Column<long>(type: "bigint", nullable: false),
                    AddedItems = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalWorking = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConditionType = table.Column<short>(type: "smallint", nullable: false),
                    DesOfAddingItems = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UseItemForAdd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldsAdding = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItemsAddingToFB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblItemsAddingToFB_tblItemsHasCondition_ConditionContext_ItemsHasCondition_ConditionContextId",
                        column: x => x.ItemsHasCondition_ConditionContextId,
                        principalTable: "tblItemsHasCondition_ConditionContext",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblItemsHasConditionAddedToFB",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FBShomareh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarAvordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Meghdar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemsHasCondition_ConditionContextId = table.Column<long>(type: "bigint", nullable: false),
                    ConditionGroupId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItemsHasConditionAddedToFB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblItemsHasConditionAddedToFB_tblBaravordUser_BarAvordId",
                        column: x => x.BarAvordId,
                        principalTable: "tblBaravordUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblItemsHasConditionAddedToFB_tblConditionGroup_ConditionGroupId",
                        column: x => x.ConditionGroupId,
                        principalTable: "tblConditionGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblItemsHasConditionAddedToFB_tblItemsHasCondition_ConditionContext_ItemsHasCondition_ConditionContextId",
                        column: x => x.ItemsHasCondition_ConditionContextId,
                        principalTable: "tblItemsHasCondition_ConditionContext",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSubItemsAddingToFB",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemsAddingToFBId = table.Column<long>(type: "bigint", nullable: false),
                    AddedItems = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalWorking = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConditionType = table.Column<int>(type: "int", nullable: false),
                    DesOfAddingItems = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldsAdding = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSubItemsAddingToFB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSubItemsAddingToFB_tblItemsAddingToFB_ItemsAddingToFBId",
                        column: x => x.ItemsAddingToFBId,
                        principalTable: "tblItemsAddingToFB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tblAbadeKoole_HadAksarErtefaKooleId",
                table: "tblAbadeKoole",
                column: "HadAksarErtefaKooleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAbroDaliDetails_AbadeKooleId",
                table: "tblAbroDaliDetails",
                column: "AbadeKooleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAmalyateKhakiInfoForBarAvord_BaravordUserId",
                table: "tblAmalyateKhakiInfoForBarAvord",
                column: "BaravordUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAmalyateKhakiInfoForBarAvordDetails_AmalyateKhakiInfoForBarAvordId",
                table: "tblAmalyateKhakiInfoForBarAvordDetails",
                column: "AmalyateKhakiInfoForBarAvordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAmalyateKhakiInfoForBarAvordDetailsEzafeBaha_AmalyateKhakiInfoForBarAvordDetailsId",
                table: "tblAmalyateKhakiInfoForBarAvordDetailsEzafeBaha",
                column: "AmalyateKhakiInfoForBarAvordDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAmalyateKhakiInfoForBarAvordDetailsMore_AmalyateKhakiInfoForBarAvordDetailsId",
                table: "tblAmalyateKhakiInfoForBarAvordDetailsMore",
                column: "AmalyateKhakiInfoForBarAvordDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAmalyateKhakiInfoForBarAvordMore_AmalyateKhakiInfoForBarAvordId",
                table: "tblAmalyateKhakiInfoForBarAvordMore",
                column: "AmalyateKhakiInfoForBarAvordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAppOperationInfoDetails_AppOperationInfoMainId",
                table: "tblAppOperationInfoDetails",
                column: "AppOperationInfoMainId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAppOperationInfoDetails_QuetionId",
                table: "tblAppOperationInfoDetails",
                column: "QuetionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAppOperationInfoMain_OperationId",
                table: "tblAppOperationInfoMain",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAppQuestion_OperationId",
                table: "tblAppQuestion",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBaseInfo_TypeId",
                table: "tblBaseInfo",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblConditionContext_ConditionGroupId",
                table: "tblConditionContext",
                column: "ConditionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_tblDastakPolInfo_PolVaAbroId",
                table: "tblDastakPolInfo",
                column: "PolVaAbroId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFB_BarAvordId",
                table: "tblFB",
                column: "BarAvordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblGeneralProjectTiming_ProjectId",
                table: "tblGeneralProjectTiming",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItemsAddingToFB_ItemsHasCondition_ConditionContextId",
                table: "tblItemsAddingToFB",
                column: "ItemsHasCondition_ConditionContextId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItemsFBDependQuestionForAbnieFani_QuesForAbnieFaniId",
                table: "tblItemsFBDependQuestionForAbnieFani",
                column: "QuesForAbnieFaniId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItemsFBShomarehValueShomareh_BarAvordId",
                table: "tblItemsFBShomarehValueShomareh",
                column: "BarAvordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItemsHasCondition_ConditionContext_ConditionContextId",
                table: "tblItemsHasCondition_ConditionContext",
                column: "ConditionContextId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItemsHasCondition_ConditionContext_ItemsHasConditionId",
                table: "tblItemsHasCondition_ConditionContext",
                column: "ItemsHasConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItemsHasCondition_ConditionContext_ParentId",
                table: "tblItemsHasCondition_ConditionContext",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItemsHasConditionAddedToFB_BarAvordId",
                table: "tblItemsHasConditionAddedToFB",
                column: "BarAvordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItemsHasConditionAddedToFB_ConditionGroupId",
                table: "tblItemsHasConditionAddedToFB",
                column: "ConditionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItemsHasConditionAddedToFB_ItemsHasCondition_ConditionContextId",
                table: "tblItemsHasConditionAddedToFB",
                column: "ItemsHasCondition_ConditionContextId");

            migrationBuilder.CreateIndex(
                name: "IX_tblKiloMetrazhOfHaml_BarAvordId",
                table: "tblKiloMetrazhOfHaml",
                column: "BarAvordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblKiloMetrazhOfHaml_OperationOfHamlId",
                table: "tblKiloMetrazhOfHaml",
                column: "OperationOfHamlId");

            migrationBuilder.CreateIndex(
                name: "IX_tblNiroZaribKarKard_NoeMashinAlatId",
                table: "tblNiroZaribKarKard",
                column: "NoeMashinAlatId");

            migrationBuilder.CreateIndex(
                name: "IX_tblNiroZaribKarKard_OperationId",
                table: "tblNiroZaribKarKard",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_tblOperation_ParentId",
                table: "tblOperation",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_tblOperation_ItemsFB_OperationId",
                table: "tblOperation_ItemsFB",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_tblOperationAmelMoaser_OperationId",
                table: "tblOperationAmelMoaser",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_tblOperationHasAddedOperations_AddedOperationId",
                table: "tblOperationHasAddedOperations",
                column: "AddedOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_tblOperationHasAddedOperations_OperationId",
                table: "tblOperationHasAddedOperations",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_tblOperationsOfHaml_ItemsHasHaml_ItemsHasHamlId",
                table: "tblOperationsOfHaml_ItemsHasHaml",
                column: "ItemsHasHamlId");

            migrationBuilder.CreateIndex(
                name: "IX_tblOperationsOfHaml_ItemsHasHaml_OperationsOfHamlId",
                table: "tblOperationsOfHaml_ItemsHasHaml",
                column: "OperationsOfHamlId");

            migrationBuilder.CreateIndex(
                name: "IX_tblOperationsOfHamlAndItems_OperationsOfHamlId",
                table: "tblOperationsOfHamlAndItems",
                column: "OperationsOfHamlId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPolVaAbroBarAvord_BarAvordId",
                table: "tblPolVaAbroBarAvord",
                column: "BarAvordId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProjectCalendar_GeneralProjectTimingId",
                table: "tblProjectCalendar",
                column: "GeneralProjectTimingId");

            migrationBuilder.CreateIndex(
                name: "IX_tblQuesForAbnieFaniValues_PolVaAbroId",
                table: "tblQuesForAbnieFaniValues",
                column: "PolVaAbroId");

            migrationBuilder.CreateIndex(
                name: "IX_tblQuesForAbnieFaniValues_QuestionForAbnieFaniId",
                table: "tblQuesForAbnieFaniValues",
                column: "QuestionForAbnieFaniId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRizKiloMetrazhOfHaml_KiloMetrazhOfHamlId",
                table: "tblRizKiloMetrazhOfHaml",
                column: "KiloMetrazhOfHamlId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRizMetreUsers_FBId",
                table: "tblRizMetreUsers",
                column: "FBId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRizMetreUsers_OperationsOfHamlId",
                table: "tblRizMetreUsers",
                column: "OperationsOfHamlId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSegmentsFromGEODB_OperationId",
                table: "tblSegmentsFromGEODB",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSegmentsFromGEODB_ProjectId",
                table: "tblSegmentsFromGEODB",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_tblShomarehFBForQuesForAbnieFani_QuesForAbnieFaniId",
                table: "tblShomarehFBForQuesForAbnieFani",
                column: "QuesForAbnieFaniId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSubItemsAddingToFB_ItemsAddingToFBId",
                table: "tblSubItemsAddingToFB",
                column: "ItemsAddingToFBId");

            migrationBuilder.CreateIndex(
                name: "IX_tblZarayebTabdil_OperationsOfHamlId",
                table: "tblZarayebTabdil",
                column: "OperationsOfHamlId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "tblAbadDivarBali");

            migrationBuilder.DropTable(
                name: "tblAbroDaliDetails");

            migrationBuilder.DropTable(
                name: "tblAmalyateKhakiInfoForBarAvordDetailsEzafeBaha");

            migrationBuilder.DropTable(
                name: "tblAmalyateKhakiInfoForBarAvordDetailsMore");

            migrationBuilder.DropTable(
                name: "tblAmalyateKhakiInfoForBarAvordMore");

            migrationBuilder.DropTable(
                name: "tblAnalizBaha");

            migrationBuilder.DropTable(
                name: "tblAppOperationInfoDetails");

            migrationBuilder.DropTable(
                name: "tblBaseInfo");

            migrationBuilder.DropTable(
                name: "tblDastakPolInfo");

            migrationBuilder.DropTable(
                name: "tblFB");

            migrationBuilder.DropTable(
                name: "tblItemsFBDependQuestionForAbnieFani");

            migrationBuilder.DropTable(
                name: "tblItemsFBShomarehValueShomareh");

            migrationBuilder.DropTable(
                name: "tblItemsFields");

            migrationBuilder.DropTable(
                name: "tblItemsForGetValues");

            migrationBuilder.DropTable(
                name: "tblItemsHasConditionAddedToFB");

            migrationBuilder.DropTable(
                name: "tblNiroMasalehMashin");

            migrationBuilder.DropTable(
                name: "tblNiroZaribKarKard");

            migrationBuilder.DropTable(
                name: "tblOperation_ItemsFB");

            migrationBuilder.DropTable(
                name: "tblOperationAmelMoaser");

            migrationBuilder.DropTable(
                name: "tblOperationHasAddedOperations");

            migrationBuilder.DropTable(
                name: "tblOperationHasAddedOperationsType");

            migrationBuilder.DropTable(
                name: "tblOperationsOfHaml_ItemsHasHaml");

            migrationBuilder.DropTable(
                name: "tblOperationsOfHamlAndItems");

            migrationBuilder.DropTable(
                name: "tblProjectCalendar");

            migrationBuilder.DropTable(
                name: "tblQuesForAbnieFaniValues");

            migrationBuilder.DropTable(
                name: "tblRizKiloMetrazhOfHaml");

            migrationBuilder.DropTable(
                name: "tblRizMetreUsers");

            migrationBuilder.DropTable(
                name: "tblSegmentsFromGEODB");

            migrationBuilder.DropTable(
                name: "tblShomarehFBForQuesForAbnieFani");

            migrationBuilder.DropTable(
                name: "tblSubItemsAddingToFB");

            migrationBuilder.DropTable(
                name: "tblZarayebTabdil");

            migrationBuilder.DropTable(
                name: "tblZaribRoadType");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "tblAbadeKoole");

            migrationBuilder.DropTable(
                name: "tblAmalyateKhakiInfoForBarAvordDetails");

            migrationBuilder.DropTable(
                name: "tblAppOperationInfoMain");

            migrationBuilder.DropTable(
                name: "tblAppQuestion");

            migrationBuilder.DropTable(
                name: "tblBaseInfoType");

            migrationBuilder.DropTable(
                name: "tblMashinType");

            migrationBuilder.DropTable(
                name: "tblItemsHasHaml");

            migrationBuilder.DropTable(
                name: "tblGeneralProjectTiming");

            migrationBuilder.DropTable(
                name: "tblPolVaAbroBarAvord");

            migrationBuilder.DropTable(
                name: "tblKiloMetrazhOfHaml");

            migrationBuilder.DropTable(
                name: "tblFehrestBaha");

            migrationBuilder.DropTable(
                name: "tblQuesForAbnieFani");

            migrationBuilder.DropTable(
                name: "tblItemsAddingToFB");

            migrationBuilder.DropTable(
                name: "tblHadAksarErtefaKoole");

            migrationBuilder.DropTable(
                name: "tblAmalyateKhakiInfoForBarAvord");

            migrationBuilder.DropTable(
                name: "tblOperation");

            migrationBuilder.DropTable(
                name: "tblProject");

            migrationBuilder.DropTable(
                name: "tblOperationsOfHaml");

            migrationBuilder.DropTable(
                name: "tblItemsHasCondition_ConditionContext");

            migrationBuilder.DropTable(
                name: "tblBaravordUser");

            migrationBuilder.DropTable(
                name: "tblConditionContext");

            migrationBuilder.DropTable(
                name: "tblItemsHasCondition");

            migrationBuilder.DropTable(
                name: "tblConditionGroup");
        }
    }
}
