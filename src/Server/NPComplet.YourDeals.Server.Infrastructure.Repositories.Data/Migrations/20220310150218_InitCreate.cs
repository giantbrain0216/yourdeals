using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NPComplet.YourDeals.Server.Infrastructure.Repositories.Data.Migrations
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SHARED");

            migrationBuilder.EnsureSchema(
                name: "PRICING");

            migrationBuilder.EnsureSchema(
                name: "FINANCE");

            migrationBuilder.EnsureSchema(
                name: "MARKET");

            migrationBuilder.EnsureSchema(
                name: "DEAL");

            migrationBuilder.EnsureSchema(
                name: "FINNACE");

            migrationBuilder.EnsureSchema(
                name: "DOCUMENTS");

            migrationBuilder.EnsureSchema(
                name: "ACCOUNTING");

            migrationBuilder.EnsureSchema(
                name: "MONITORING");

            migrationBuilder.EnsureSchema(
                name: "COMMUNICATION");

            migrationBuilder.EnsureSchema(
                name: "USER");

            migrationBuilder.EnsureSchema(
                name: "DOCUMENT");

            migrationBuilder.EnsureSchema(
                name: "SEARCH");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditTrails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffectedColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeletedAccount = table.Column<bool>(type: "bit", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "COLLECTION_MARKETS",
                schema: "MARKET",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COLLECTION_MARKETS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COLLECTION_MARKETS_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "COMPANY",
                schema: "SHARED",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LegalForm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxIdentifierNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPANY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COMPANY_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CURRENCIES",
                schema: "PRICING",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CURRENCIES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CURRENCIES_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DEALDOCUMENTS",
                schema: "DEAL",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEALDOCUMENTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DEALDOCUMENTS_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FEESTIERS",
                schema: "FINNACE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FEESTIERS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FEESTIERS_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GateWayOperationPayments",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateWayOperationPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateWayOperationPayments_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GateWayOperationPayouts",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateWayOperationPayouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateWayOperationPayouts_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LOCATIONS",
                schema: "SHARED",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Radius = table.Column<float>(type: "real", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    StateCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOCATIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LOCATIONS_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LOG",
                schema: "MONITORING",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logger = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thread = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOG", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LOG_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "POSTS",
                schema: "SHARED",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POSTS_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PROPOSITIONSFINANCEOPERATIONS",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROPOSITIONSFINANCEOPERATIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PROPOSITIONSFINANCEOPERATIONS_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SERVICEGATEWAY",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceGatewayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApplicationDefault = table.Column<bool>(type: "bit", nullable: false),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SERVICEGATEWAY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SERVICEGATEWAY_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "USERPREFERENCEFINANCE",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERPREFERENCEFINANCE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USERPREFERENCEFINANCE_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MARKETS",
                schema: "MARKET",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MarketEndTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MarketsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MarketStartTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpeningTimeSpan = table.Column<TimeSpan>(type: "time", nullable: false),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MARKETS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MARKETS_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MARKETS_COLLECTION_MARKETS_MarketsId",
                        column: x => x.MarketsId,
                        principalSchema: "MARKET",
                        principalTable: "COLLECTION_MARKETS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UNITPRICINGS",
                schema: "PRICING",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Taxes = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UNITPRICINGS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UNITPRICINGS_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UNITPRICINGS_CURRENCIES_CurrencyCodeId",
                        column: x => x.CurrencyCodeId,
                        principalSchema: "PRICING",
                        principalTable: "CURRENCIES",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "STOREDFILES",
                schema: "DOCUMENT",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STOREDFILES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STOREDFILES_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STOREDFILES_DEALDOCUMENTS_DealDocumentId",
                        column: x => x.DealDocumentId,
                        principalSchema: "DEAL",
                        principalTable: "DEALDOCUMENTS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DEALFEESTIERS",
                schema: "FINNACE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MoneyLimit = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEALFEESTIERS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DEALFEESTIERS_FEESTIERS_Id",
                        column: x => x.Id,
                        principalSchema: "FINNACE",
                        principalTable: "FEESTIERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ADDRESSES",
                schema: "SHARED",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressType = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HouseNumber = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NumCountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneCountyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADDRESSES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ADDRESSES_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ADDRESSES_LOCATIONS_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "SHARED",
                        principalTable: "LOCATIONS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DEALMESSAGESPOSTS",
                schema: "DOCUMENTS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEALMESSAGESPOSTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DEALMESSAGESPOSTS_POSTS_Id",
                        column: x => x.Id,
                        principalSchema: "SHARED",
                        principalTable: "POSTS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PROPOSITIONMESSAGESPOSTS",
                schema: "DOCUMENTS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROPOSITIONMESSAGESPOSTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PROPOSITIONMESSAGESPOSTS_POSTS_Id",
                        column: x => x.Id,
                        principalSchema: "SHARED",
                        principalTable: "POSTS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CYBERSOURCESERVICEGATEWAY",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CYBERSOURCESERVICEGATEWAY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CYBERSOURCESERVICEGATEWAY_SERVICEGATEWAY_Id",
                        column: x => x.Id,
                        principalSchema: "FINANCE",
                        principalTable: "SERVICEGATEWAY",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FINANCESUPPORTS",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AliasName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinanceSupportName = table.Column<int>(type: "int", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    UserPreferenceFinanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FINANCESUPPORTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FINANCESUPPORTS_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FINANCESUPPORTS_USERPREFERENCEFINANCE_UserPreferenceFinanceId",
                        column: x => x.UserPreferenceFinanceId,
                        principalSchema: "FINANCE",
                        principalTable: "USERPREFERENCEFINANCE",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PROFILEIMAGES",
                schema: "USER",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROFILEIMAGES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PROFILEIMAGES_STOREDFILES_Id",
                        column: x => x.Id,
                        principalSchema: "DOCUMENT",
                        principalTable: "STOREDFILES",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SIGNATURE",
                schema: "SHARED",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIGNATURE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SIGNATURE_STOREDFILES_Id",
                        column: x => x.Id,
                        principalSchema: "DOCUMENT",
                        principalTable: "STOREDFILES",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "INVOICES",
                schema: "ACCOUNTING",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClientAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternalUniqueNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PrintDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INVOICES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INVOICES_ADDRESSES_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "SHARED",
                        principalTable: "ADDRESSES",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_INVOICES_ADDRESSES_ClientAddressId",
                        column: x => x.ClientAddressId,
                        principalSchema: "SHARED",
                        principalTable: "ADDRESSES",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_INVOICES_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_INVOICES_COMPANY_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "SHARED",
                        principalTable: "COMPANY",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_INVOICES_POSTS_PostId",
                        column: x => x.PostId,
                        principalSchema: "SHARED",
                        principalTable: "POSTS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MESSAGES",
                schema: "COMMUNICATION",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsRevivedUserSeen = table.Column<bool>(type: "bit", nullable: false),
                    MessageCoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SendingTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropositionMessagesPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DealMessagesPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MESSAGES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MESSAGES_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MESSAGES_AspNetUsers_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MESSAGES_DEALMESSAGESPOSTS_DealMessagesPostId",
                        column: x => x.DealMessagesPostId,
                        principalSchema: "DOCUMENTS",
                        principalTable: "DEALMESSAGESPOSTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MESSAGES_POSTS_MessageCoreId",
                        column: x => x.MessageCoreId,
                        principalSchema: "SHARED",
                        principalTable: "POSTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MESSAGES_PROPOSITIONMESSAGESPOSTS_PropositionMessagesPostId",
                        column: x => x.PropositionMessagesPostId,
                        principalSchema: "DOCUMENTS",
                        principalTable: "PROPOSITIONMESSAGESPOSTS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BANKACCOUNTS",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BIC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IBAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BANKACCOUNTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BANKACCOUNTS_FINANCESUPPORTS_Id",
                        column: x => x.Id,
                        principalSchema: "FINANCE",
                        principalTable: "FINANCESUPPORTS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CREDITCARDS",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillingAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardHolderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cvv2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpireMonth = table.Column<int>(type: "int", nullable: false),
                    ExpireYear = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidUntilUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CREDITCARDS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CREDITCARDS_ADDRESSES_BillingAddressId",
                        column: x => x.BillingAddressId,
                        principalSchema: "SHARED",
                        principalTable: "ADDRESSES",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CREDITCARDS_FINANCESUPPORTS_Id",
                        column: x => x.Id,
                        principalSchema: "FINANCE",
                        principalTable: "FINANCESUPPORTS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PAYPALACCOUNT",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayPalMerchantID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAYPALACCOUNT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PAYPALACCOUNT_FINANCESUPPORTS_Id",
                        column: x => x.Id,
                        principalSchema: "FINANCE",
                        principalTable: "FINANCESUPPORTS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PROFILES",
                schema: "USER",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserPreferenceFinanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProfileImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROFILES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PROFILES_ADDRESSES_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "SHARED",
                        principalTable: "ADDRESSES",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PROFILES_PROFILEIMAGES_ProfileImageId",
                        column: x => x.ProfileImageId,
                        principalSchema: "USER",
                        principalTable: "PROFILEIMAGES",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PROFILES_USERPREFERENCEFINANCE_UserPreferenceFinanceId",
                        column: x => x.UserPreferenceFinanceId,
                        principalSchema: "FINANCE",
                        principalTable: "USERPREFERENCEFINANCE",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QUOTATIONS",
                schema: "ACCOUNTING",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClientAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientSignatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalUniqueNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PrintDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TermConditionPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WarrantyPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QUOTATIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QUOTATIONS_ADDRESSES_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "SHARED",
                        principalTable: "ADDRESSES",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QUOTATIONS_ADDRESSES_ClientAddressId",
                        column: x => x.ClientAddressId,
                        principalSchema: "SHARED",
                        principalTable: "ADDRESSES",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QUOTATIONS_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QUOTATIONS_COMPANY_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "SHARED",
                        principalTable: "COMPANY",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QUOTATIONS_POSTS_PostId",
                        column: x => x.PostId,
                        principalSchema: "SHARED",
                        principalTable: "POSTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QUOTATIONS_POSTS_TermConditionPostId",
                        column: x => x.TermConditionPostId,
                        principalSchema: "SHARED",
                        principalTable: "POSTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QUOTATIONS_POSTS_WarrantyPostId",
                        column: x => x.WarrantyPostId,
                        principalSchema: "SHARED",
                        principalTable: "POSTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QUOTATIONS_SIGNATURE_ClientSignatureId",
                        column: x => x.ClientSignatureId,
                        principalSchema: "SHARED",
                        principalTable: "SIGNATURE",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AMOUNTS",
                schema: "PRICING",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AmountValue = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuotationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AMOUNTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AMOUNTS_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AMOUNTS_INVOICES_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "ACCOUNTING",
                        principalTable: "INVOICES",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AMOUNTS_QUOTATIONS_QuotationId",
                        column: x => x.QuotationId,
                        principalSchema: "ACCOUNTING",
                        principalTable: "QUOTATIONS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AMOUNTS_UNITPRICINGS_AmountUnitId",
                        column: x => x.AmountUnitId,
                        principalSchema: "PRICING",
                        principalTable: "UNITPRICINGS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DEALPRICEREFERENCES",
                schema: "PRICING",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEALPRICEREFERENCES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DEALPRICEREFERENCES_AMOUNTS_Id",
                        column: x => x.Id,
                        principalSchema: "PRICING",
                        principalTable: "AMOUNTS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FINANCEOPERATIONS",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinanceOperationType = table.Column<int>(type: "int", nullable: false),
                    AmountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FinanceOperationState = table.Column<int>(type: "int", nullable: false),
                    ServiceGatewayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceGatewayName = table.Column<int>(type: "int", nullable: false),
                    FinanceSupportId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FinanceSupportName = table.Column<int>(type: "int", nullable: false),
                    ParentFinanceOperationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FINANCEOPERATIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FINANCEOPERATIONS_AMOUNTS_AmountId",
                        column: x => x.AmountId,
                        principalSchema: "PRICING",
                        principalTable: "AMOUNTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FINANCEOPERATIONS_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FINANCEOPERATIONS_FINANCEOPERATIONS_ParentFinanceOperationId",
                        column: x => x.ParentFinanceOperationId,
                        principalSchema: "FINANCE",
                        principalTable: "FINANCEOPERATIONS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FINANCEOPERATIONS_FINANCESUPPORTS_FinanceSupportId",
                        column: x => x.FinanceSupportId,
                        principalSchema: "FINANCE",
                        principalTable: "FINANCESUPPORTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FINANCEOPERATIONS_SERVICEGATEWAY_ServiceGatewayId",
                        column: x => x.ServiceGatewayId,
                        principalSchema: "FINANCE",
                        principalTable: "SERVICEGATEWAY",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "POSTDEALFEESTIERS",
                schema: "FINNACE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSTDEALFEESTIERS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POSTDEALFEESTIERS_AMOUNTS_AmountId",
                        column: x => x.AmountId,
                        principalSchema: "PRICING",
                        principalTable: "AMOUNTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_POSTDEALFEESTIERS_FEESTIERS_Id",
                        column: x => x.Id,
                        principalSchema: "FINNACE",
                        principalTable: "FEESTIERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PROPOSITIONS",
                schema: "DEAL",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsSelected = table.Column<bool>(type: "bit", nullable: true),
                    PropositionAmountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentManor = table.Column<int>(type: "int", nullable: true),
                    PropositionMessagesPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PropositionsFinanceOperationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROPOSITIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PROPOSITIONS_AMOUNTS_PropositionAmountId",
                        column: x => x.PropositionAmountId,
                        principalSchema: "PRICING",
                        principalTable: "AMOUNTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PROPOSITIONS_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PROPOSITIONS_PROPOSITIONMESSAGESPOSTS_PropositionMessagesPostId",
                        column: x => x.PropositionMessagesPostId,
                        principalSchema: "DOCUMENTS",
                        principalTable: "PROPOSITIONMESSAGESPOSTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PROPOSITIONS_PROPOSITIONSFINANCEOPERATIONS_PropositionsFinanceOperationId",
                        column: x => x.PropositionsFinanceOperationId,
                        principalSchema: "FINANCE",
                        principalTable: "PROPOSITIONSFINANCEOPERATIONS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DEALPAYMENTMANORS",
                schema: "PRICING",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentManor = table.Column<int>(type: "int", nullable: false),
                    DealPriceReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEALPAYMENTMANORS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DEALPAYMENTMANORS_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DEALPAYMENTMANORS_DEALPRICEREFERENCES_DealPriceReferenceId",
                        column: x => x.DealPriceReferenceId,
                        principalSchema: "PRICING",
                        principalTable: "DEALPRICEREFERENCES",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CYBERSORUCEPAYMENTS",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinanceOperationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FinanceGateWayOperationState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CYBERSORUCEPAYMENTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CYBERSORUCEPAYMENTS_FINANCEOPERATIONS_FinanceOperationId",
                        column: x => x.FinanceOperationId,
                        principalSchema: "FINANCE",
                        principalTable: "FINANCEOPERATIONS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CYBERSORUCEPAYMENTS_GateWayOperationPayments_Id",
                        column: x => x.Id,
                        principalSchema: "FINANCE",
                        principalTable: "GateWayOperationPayments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CYBERSORUCEPAYOUTS",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinanceOperationType = table.Column<int>(type: "int", nullable: false),
                    FinanceOperationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FinanceGateWayOperationState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CYBERSORUCEPAYOUTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CYBERSORUCEPAYOUTS_FINANCEOPERATIONS_FinanceOperationId",
                        column: x => x.FinanceOperationId,
                        principalSchema: "FINANCE",
                        principalTable: "FINANCEOPERATIONS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CYBERSORUCEPAYOUTS_GateWayOperationPayouts_Id",
                        column: x => x.Id,
                        principalSchema: "FINANCE",
                        principalTable: "GateWayOperationPayouts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GateWayOperationRequests",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FiniancialOpreationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinanceOperationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateWayOperationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateWayOperationRequests_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GateWayOperationRequests_FINANCEOPERATIONS_FinanceOperationId",
                        column: x => x.FinanceOperationId,
                        principalSchema: "FINANCE",
                        principalTable: "FINANCEOPERATIONS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GateWayOperationResponses",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FiniancialOpreationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinanceOperationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateWayOperationResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateWayOperationResponses_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GateWayOperationResponses_FINANCEOPERATIONS_FinanceOperationId",
                        column: x => x.FinanceOperationId,
                        principalSchema: "FINANCE",
                        principalTable: "FINANCEOPERATIONS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NPCOMPLETEARNINGSWALLET",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinanceOperationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CashFlow = table.Column<int>(type: "int", nullable: false),
                    AmountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AvailableBlance = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPCOMPLETEARNINGSWALLET", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NPCOMPLETEARNINGSWALLET_AMOUNTS_AmountId",
                        column: x => x.AmountId,
                        principalSchema: "PRICING",
                        principalTable: "AMOUNTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NPCOMPLETEARNINGSWALLET_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NPCOMPLETEARNINGSWALLET_FINANCEOPERATIONS_FinanceOperationId",
                        column: x => x.FinanceOperationId,
                        principalSchema: "FINANCE",
                        principalTable: "FINANCEOPERATIONS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "USEREARNINGSWALLET",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinanceOperationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CashFlow = table.Column<int>(type: "int", nullable: false),
                    AmountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AvailableBlance = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USEREARNINGSWALLET", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USEREARNINGSWALLET_AMOUNTS_AmountId",
                        column: x => x.AmountId,
                        principalSchema: "PRICING",
                        principalTable: "AMOUNTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_USEREARNINGSWALLET_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_USEREARNINGSWALLET_FINANCEOPERATIONS_FinanceOperationId",
                        column: x => x.FinanceOperationId,
                        principalSchema: "FINANCE",
                        principalTable: "FINANCEOPERATIONS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CYBERSORUCEPAYMENTSREQUEST",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CaptureTrueForProcessPayment = table.Column<bool>(type: "bit", nullable: false),
                    CreditCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BankAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CallState = table.Column<int>(type: "int", nullable: false),
                    CybersourcePaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CYBERSORUCEPAYMENTSREQUEST", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CYBERSORUCEPAYMENTSREQUEST_BANKACCOUNTS_BankAccountId",
                        column: x => x.BankAccountId,
                        principalSchema: "FINANCE",
                        principalTable: "BANKACCOUNTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CYBERSORUCEPAYMENTSREQUEST_CREDITCARDS_CreditCardId",
                        column: x => x.CreditCardId,
                        principalSchema: "FINANCE",
                        principalTable: "CREDITCARDS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CYBERSORUCEPAYMENTSREQUEST_CYBERSORUCEPAYMENTS_CybersourcePaymentId",
                        column: x => x.CybersourcePaymentId,
                        principalSchema: "FINANCE",
                        principalTable: "CYBERSORUCEPAYMENTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CYBERSORUCEPAYMENTSREQUEST_GateWayOperationRequests_Id",
                        column: x => x.Id,
                        principalSchema: "FINANCE",
                        principalTable: "GateWayOperationRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CYBERSORUCEPAYOUTSREQUEST",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreditCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BankAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CallState = table.Column<int>(type: "int", nullable: false),
                    CybersourcePayoutId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CYBERSORUCEPAYOUTSREQUEST", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CYBERSORUCEPAYOUTSREQUEST_BANKACCOUNTS_BankAccountId",
                        column: x => x.BankAccountId,
                        principalSchema: "FINANCE",
                        principalTable: "BANKACCOUNTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CYBERSORUCEPAYOUTSREQUEST_CREDITCARDS_CreditCardId",
                        column: x => x.CreditCardId,
                        principalSchema: "FINANCE",
                        principalTable: "CREDITCARDS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CYBERSORUCEPAYOUTSREQUEST_CYBERSORUCEPAYOUTS_CybersourcePayoutId",
                        column: x => x.CybersourcePayoutId,
                        principalSchema: "FINANCE",
                        principalTable: "CYBERSORUCEPAYOUTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CYBERSORUCEPAYOUTSREQUEST_GateWayOperationRequests_Id",
                        column: x => x.Id,
                        principalSchema: "FINANCE",
                        principalTable: "GateWayOperationRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CYBERSORUCEPAYMENTSRESPONSE",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CallState = table.Column<int>(type: "int", nullable: false),
                    CybersourcePaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CYBERSORUCEPAYMENTSRESPONSE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CYBERSORUCEPAYMENTSRESPONSE_CYBERSORUCEPAYMENTS_CybersourcePaymentId",
                        column: x => x.CybersourcePaymentId,
                        principalSchema: "FINANCE",
                        principalTable: "CYBERSORUCEPAYMENTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CYBERSORUCEPAYMENTSRESPONSE_GateWayOperationResponses_Id",
                        column: x => x.Id,
                        principalSchema: "FINANCE",
                        principalTable: "GateWayOperationResponses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CYBERSORUCEPAYOUTSRESPONSE",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CallState = table.Column<int>(type: "int", nullable: false),
                    CybersourcePayoutId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CYBERSORUCEPAYOUTSRESPONSE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CYBERSORUCEPAYOUTSRESPONSE_CYBERSORUCEPAYOUTS_CybersourcePayoutId",
                        column: x => x.CybersourcePayoutId,
                        principalSchema: "FINANCE",
                        principalTable: "CYBERSORUCEPAYOUTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CYBERSORUCEPAYOUTSRESPONSE_GateWayOperationResponses_Id",
                        column: x => x.Id,
                        principalSchema: "FINANCE",
                        principalTable: "GateWayOperationResponses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CROSSINGPAYOUTS",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DealsFinanceOpertationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PropositionsFinanceOperationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CROSSINGPAYOUTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CROSSINGPAYOUTS_FINANCEOPERATIONS_Id",
                        column: x => x.Id,
                        principalSchema: "FINANCE",
                        principalTable: "FINANCEOPERATIONS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CROSSINGPAYOUTS_PROPOSITIONSFINANCEOPERATIONS_PropositionsFinanceOperationId",
                        column: x => x.PropositionsFinanceOperationId,
                        principalSchema: "FINANCE",
                        principalTable: "PROPOSITIONSFINANCEOPERATIONS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DEALS",
                schema: "DEAL",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DealDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DealMessagesPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DealPriceReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDone = table.Column<bool>(type: "bit", nullable: true),
                    IsLocked = table.Column<bool>(type: "bit", nullable: true),
                    IsSelected = table.Column<bool>(type: "bit", nullable: true),
                    DealsFinanceOpertationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MarketId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEALS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DEALS_ADDRESSES_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "SHARED",
                        principalTable: "ADDRESSES",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DEALS_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DEALS_DEALDOCUMENTS_DealDocumentId",
                        column: x => x.DealDocumentId,
                        principalSchema: "DEAL",
                        principalTable: "DEALDOCUMENTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DEALS_DEALMESSAGESPOSTS_DealMessagesPostId",
                        column: x => x.DealMessagesPostId,
                        principalSchema: "DOCUMENTS",
                        principalTable: "DEALMESSAGESPOSTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DEALS_DEALPRICEREFERENCES_DealPriceReferenceId",
                        column: x => x.DealPriceReferenceId,
                        principalSchema: "PRICING",
                        principalTable: "DEALPRICEREFERENCES",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DEALS_MARKETS_MarketId",
                        column: x => x.MarketId,
                        principalSchema: "MARKET",
                        principalTable: "MARKETS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OFFERS",
                schema: "DEAL",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuotationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OFFERS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OFFERS_DEALS_Id",
                        column: x => x.Id,
                        principalSchema: "DEAL",
                        principalTable: "DEALS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OFFERS_QUOTATIONS_QuotationId",
                        column: x => x.QuotationId,
                        principalSchema: "ACCOUNTING",
                        principalTable: "QUOTATIONS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "POSTINGDEALPAYMENTS",
                schema: "FINNACE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DealId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSTINGDEALPAYMENTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POSTINGDEALPAYMENTS_DEALS_DealId",
                        column: x => x.DealId,
                        principalSchema: "DEAL",
                        principalTable: "DEALS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POSTINGDEALPAYMENTS_FINANCEOPERATIONS_Id",
                        column: x => x.Id,
                        principalSchema: "FINANCE",
                        principalTable: "FINANCEOPERATIONS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "REQUESTS",
                schema: "DEAL",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REQUESTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_REQUESTS_DEALS_Id",
                        column: x => x.Id,
                        principalSchema: "DEAL",
                        principalTable: "DEALS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_REQUESTS_INVOICES_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "ACCOUNTING",
                        principalTable: "INVOICES",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TAGS",
                schema: "SEARCH",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAGS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TAGS_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TAGS_DEALS_DealId",
                        column: x => x.DealId,
                        principalSchema: "DEAL",
                        principalTable: "DEALS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PROPOSITIONOFFERS",
                schema: "DEAL",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OfferId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROPOSITIONOFFERS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PROPOSITIONOFFERS_INVOICES_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "ACCOUNTING",
                        principalTable: "INVOICES",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PROPOSITIONOFFERS_OFFERS_OfferId",
                        column: x => x.OfferId,
                        principalSchema: "DEAL",
                        principalTable: "OFFERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PROPOSITIONOFFERS_OFFERS_OfferId1",
                        column: x => x.OfferId1,
                        principalSchema: "DEAL",
                        principalTable: "OFFERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PROPOSITIONOFFERS_PROPOSITIONS_Id",
                        column: x => x.Id,
                        principalSchema: "DEAL",
                        principalTable: "PROPOSITIONS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DEALSFINANCEOPERATIONS",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostingDealPaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternalApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalCreationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalModificationDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalValidation = table.Column<bool>(type: "bit", nullable: false),
                    InternalIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEALSFINANCEOPERATIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DEALSFINANCEOPERATIONS_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DEALSFINANCEOPERATIONS_POSTINGDEALPAYMENTS_PostingDealPaymentId",
                        column: x => x.PostingDealPaymentId,
                        principalSchema: "FINNACE",
                        principalTable: "POSTINGDEALPAYMENTS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PROPOSITIONREQUESTS",
                schema: "DEAL",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuotationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROPOSITIONREQUESTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PROPOSITIONREQUESTS_DEALDOCUMENTS_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "DEAL",
                        principalTable: "DEALDOCUMENTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PROPOSITIONREQUESTS_PROPOSITIONS_Id",
                        column: x => x.Id,
                        principalSchema: "DEAL",
                        principalTable: "PROPOSITIONS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PROPOSITIONREQUESTS_QUOTATIONS_QuotationId",
                        column: x => x.QuotationId,
                        principalSchema: "ACCOUNTING",
                        principalTable: "QUOTATIONS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PROPOSITIONREQUESTS_REQUESTS_RequestId",
                        column: x => x.RequestId,
                        principalSchema: "DEAL",
                        principalTable: "REQUESTS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PROPOSITIONREQUESTS_REQUESTS_RequestId1",
                        column: x => x.RequestId1,
                        principalSchema: "DEAL",
                        principalTable: "REQUESTS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CROSSINGPAYMENTS",
                schema: "FINANCE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DealId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DealType = table.Column<int>(type: "int", nullable: false),
                    PropositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    DealsFinanceOpertationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PropositionsFinanceOperationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CROSSINGPAYMENTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CROSSINGPAYMENTS_DEALS_DealId",
                        column: x => x.DealId,
                        principalSchema: "DEAL",
                        principalTable: "DEALS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CROSSINGPAYMENTS_DEALSFINANCEOPERATIONS_DealsFinanceOpertationId",
                        column: x => x.DealsFinanceOpertationId,
                        principalSchema: "FINANCE",
                        principalTable: "DEALSFINANCEOPERATIONS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CROSSINGPAYMENTS_FINANCEOPERATIONS_Id",
                        column: x => x.Id,
                        principalSchema: "FINANCE",
                        principalTable: "FINANCEOPERATIONS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CROSSINGPAYMENTS_PROPOSITIONS_PropositionId",
                        column: x => x.PropositionId,
                        principalSchema: "DEAL",
                        principalTable: "PROPOSITIONS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CROSSINGPAYMENTS_PROPOSITIONSFINANCEOPERATIONS_PropositionsFinanceOperationId",
                        column: x => x.PropositionsFinanceOperationId,
                        principalSchema: "FINANCE",
                        principalTable: "PROPOSITIONSFINANCEOPERATIONS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ADDRESSES_LocationId",
                schema: "SHARED",
                table: "ADDRESSES",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ADDRESSES_OwnerId",
                schema: "SHARED",
                table: "ADDRESSES",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AMOUNTS_AmountUnitId",
                schema: "PRICING",
                table: "AMOUNTS",
                column: "AmountUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AMOUNTS_InvoiceId",
                schema: "PRICING",
                table: "AMOUNTS",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AMOUNTS_OwnerId",
                schema: "PRICING",
                table: "AMOUNTS",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AMOUNTS_QuotationId",
                schema: "PRICING",
                table: "AMOUNTS",
                column: "QuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId1",
                table: "AspNetRoleClaims",
                column: "RoleId1");

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
                name: "IX_AspNetUsers_ProfileId",
                table: "AspNetUsers",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_COLLECTION_MARKETS_OwnerId",
                schema: "MARKET",
                table: "COLLECTION_MARKETS",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_COMPANY_OwnerId",
                schema: "SHARED",
                table: "COMPANY",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CREDITCARDS_BillingAddressId",
                schema: "FINANCE",
                table: "CREDITCARDS",
                column: "BillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_CROSSINGPAYMENTS_DealId",
                schema: "FINANCE",
                table: "CROSSINGPAYMENTS",
                column: "DealId");

            migrationBuilder.CreateIndex(
                name: "IX_CROSSINGPAYMENTS_DealsFinanceOpertationId",
                schema: "FINANCE",
                table: "CROSSINGPAYMENTS",
                column: "DealsFinanceOpertationId");

            migrationBuilder.CreateIndex(
                name: "IX_CROSSINGPAYMENTS_PropositionId",
                schema: "FINANCE",
                table: "CROSSINGPAYMENTS",
                column: "PropositionId");

            migrationBuilder.CreateIndex(
                name: "IX_CROSSINGPAYMENTS_PropositionsFinanceOperationId",
                schema: "FINANCE",
                table: "CROSSINGPAYMENTS",
                column: "PropositionsFinanceOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_CROSSINGPAYOUTS_DealsFinanceOpertationId",
                schema: "FINANCE",
                table: "CROSSINGPAYOUTS",
                column: "DealsFinanceOpertationId");

            migrationBuilder.CreateIndex(
                name: "IX_CROSSINGPAYOUTS_PropositionsFinanceOperationId",
                schema: "FINANCE",
                table: "CROSSINGPAYOUTS",
                column: "PropositionsFinanceOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_CURRENCIES_OwnerId",
                schema: "PRICING",
                table: "CURRENCIES",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CYBERSORUCEPAYMENTS_FinanceOperationId",
                schema: "FINANCE",
                table: "CYBERSORUCEPAYMENTS",
                column: "FinanceOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_CYBERSORUCEPAYMENTSREQUEST_BankAccountId",
                schema: "FINANCE",
                table: "CYBERSORUCEPAYMENTSREQUEST",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CYBERSORUCEPAYMENTSREQUEST_CreditCardId",
                schema: "FINANCE",
                table: "CYBERSORUCEPAYMENTSREQUEST",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_CYBERSORUCEPAYMENTSREQUEST_CybersourcePaymentId",
                schema: "FINANCE",
                table: "CYBERSORUCEPAYMENTSREQUEST",
                column: "CybersourcePaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_CYBERSORUCEPAYMENTSRESPONSE_CybersourcePaymentId",
                schema: "FINANCE",
                table: "CYBERSORUCEPAYMENTSRESPONSE",
                column: "CybersourcePaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_CYBERSORUCEPAYOUTS_FinanceOperationId",
                schema: "FINANCE",
                table: "CYBERSORUCEPAYOUTS",
                column: "FinanceOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_CYBERSORUCEPAYOUTSREQUEST_BankAccountId",
                schema: "FINANCE",
                table: "CYBERSORUCEPAYOUTSREQUEST",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CYBERSORUCEPAYOUTSREQUEST_CreditCardId",
                schema: "FINANCE",
                table: "CYBERSORUCEPAYOUTSREQUEST",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_CYBERSORUCEPAYOUTSREQUEST_CybersourcePayoutId",
                schema: "FINANCE",
                table: "CYBERSORUCEPAYOUTSREQUEST",
                column: "CybersourcePayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_CYBERSORUCEPAYOUTSRESPONSE_CybersourcePayoutId",
                schema: "FINANCE",
                table: "CYBERSORUCEPAYOUTSRESPONSE",
                column: "CybersourcePayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_DEALDOCUMENTS_OwnerId",
                schema: "DEAL",
                table: "DEALDOCUMENTS",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DEALPAYMENTMANORS_DealPriceReferenceId",
                schema: "PRICING",
                table: "DEALPAYMENTMANORS",
                column: "DealPriceReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_DEALPAYMENTMANORS_OwnerId",
                schema: "PRICING",
                table: "DEALPAYMENTMANORS",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DEALS_AddressId",
                schema: "DEAL",
                table: "DEALS",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_DEALS_DealDocumentId",
                schema: "DEAL",
                table: "DEALS",
                column: "DealDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DEALS_DealMessagesPostId",
                schema: "DEAL",
                table: "DEALS",
                column: "DealMessagesPostId");

            migrationBuilder.CreateIndex(
                name: "IX_DEALS_DealPriceReferenceId",
                schema: "DEAL",
                table: "DEALS",
                column: "DealPriceReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_DEALS_DealsFinanceOpertationId",
                schema: "DEAL",
                table: "DEALS",
                column: "DealsFinanceOpertationId",
                unique: true,
                filter: "[DealsFinanceOpertationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DEALS_MarketId",
                schema: "DEAL",
                table: "DEALS",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_DEALS_OwnerId",
                schema: "DEAL",
                table: "DEALS",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DEALSFINANCEOPERATIONS_OwnerId",
                schema: "FINANCE",
                table: "DEALSFINANCEOPERATIONS",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DEALSFINANCEOPERATIONS_PostingDealPaymentId",
                schema: "FINANCE",
                table: "DEALSFINANCEOPERATIONS",
                column: "PostingDealPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_FEESTIERS_OwnerId",
                schema: "FINNACE",
                table: "FEESTIERS",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FINANCEOPERATIONS_AmountId",
                schema: "FINANCE",
                table: "FINANCEOPERATIONS",
                column: "AmountId");

            migrationBuilder.CreateIndex(
                name: "IX_FINANCEOPERATIONS_FinanceSupportId",
                schema: "FINANCE",
                table: "FINANCEOPERATIONS",
                column: "FinanceSupportId");

            migrationBuilder.CreateIndex(
                name: "IX_FINANCEOPERATIONS_OwnerId",
                schema: "FINANCE",
                table: "FINANCEOPERATIONS",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FINANCEOPERATIONS_ParentFinanceOperationId",
                schema: "FINANCE",
                table: "FINANCEOPERATIONS",
                column: "ParentFinanceOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_FINANCEOPERATIONS_ServiceGatewayId",
                schema: "FINANCE",
                table: "FINANCEOPERATIONS",
                column: "ServiceGatewayId");

            migrationBuilder.CreateIndex(
                name: "IX_FINANCESUPPORTS_OwnerId",
                schema: "FINANCE",
                table: "FINANCESUPPORTS",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FINANCESUPPORTS_UserPreferenceFinanceId",
                schema: "FINANCE",
                table: "FINANCESUPPORTS",
                column: "UserPreferenceFinanceId");

            migrationBuilder.CreateIndex(
                name: "IX_GateWayOperationPayments_OwnerId",
                schema: "FINANCE",
                table: "GateWayOperationPayments",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_GateWayOperationPayouts_OwnerId",
                schema: "FINANCE",
                table: "GateWayOperationPayouts",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_GateWayOperationRequests_FinanceOperationId",
                schema: "FINANCE",
                table: "GateWayOperationRequests",
                column: "FinanceOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_GateWayOperationRequests_OwnerId",
                schema: "FINANCE",
                table: "GateWayOperationRequests",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_GateWayOperationResponses_FinanceOperationId",
                schema: "FINANCE",
                table: "GateWayOperationResponses",
                column: "FinanceOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_GateWayOperationResponses_OwnerId",
                schema: "FINANCE",
                table: "GateWayOperationResponses",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_INVOICES_AddressId",
                schema: "ACCOUNTING",
                table: "INVOICES",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_INVOICES_ClientAddressId",
                schema: "ACCOUNTING",
                table: "INVOICES",
                column: "ClientAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_INVOICES_CompanyId",
                schema: "ACCOUNTING",
                table: "INVOICES",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_INVOICES_OwnerId",
                schema: "ACCOUNTING",
                table: "INVOICES",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_INVOICES_PostId",
                schema: "ACCOUNTING",
                table: "INVOICES",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_LOCATIONS_OwnerId",
                schema: "SHARED",
                table: "LOCATIONS",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_LOG_OwnerId",
                schema: "MONITORING",
                table: "LOG",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_MARKETS_MarketsId",
                schema: "MARKET",
                table: "MARKETS",
                column: "MarketsId");

            migrationBuilder.CreateIndex(
                name: "IX_MARKETS_OwnerId",
                schema: "MARKET",
                table: "MARKETS",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGES_DealMessagesPostId",
                schema: "COMMUNICATION",
                table: "MESSAGES",
                column: "DealMessagesPostId");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGES_MessageCoreId",
                schema: "COMMUNICATION",
                table: "MESSAGES",
                column: "MessageCoreId");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGES_OwnerId",
                schema: "COMMUNICATION",
                table: "MESSAGES",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGES_PropositionMessagesPostId",
                schema: "COMMUNICATION",
                table: "MESSAGES",
                column: "PropositionMessagesPostId");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGES_ToUserId",
                schema: "COMMUNICATION",
                table: "MESSAGES",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_NPCOMPLETEARNINGSWALLET_AmountId",
                schema: "FINANCE",
                table: "NPCOMPLETEARNINGSWALLET",
                column: "AmountId");

            migrationBuilder.CreateIndex(
                name: "IX_NPCOMPLETEARNINGSWALLET_FinanceOperationId",
                schema: "FINANCE",
                table: "NPCOMPLETEARNINGSWALLET",
                column: "FinanceOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_NPCOMPLETEARNINGSWALLET_OwnerId",
                schema: "FINANCE",
                table: "NPCOMPLETEARNINGSWALLET",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_OFFERS_QuotationId",
                schema: "DEAL",
                table: "OFFERS",
                column: "QuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_POSTDEALFEESTIERS_AmountId",
                schema: "FINNACE",
                table: "POSTDEALFEESTIERS",
                column: "AmountId");

            migrationBuilder.CreateIndex(
                name: "IX_POSTINGDEALPAYMENTS_DealId",
                schema: "FINNACE",
                table: "POSTINGDEALPAYMENTS",
                column: "DealId");

            migrationBuilder.CreateIndex(
                name: "IX_POSTS_OwnerId",
                schema: "SHARED",
                table: "POSTS",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PROFILES_AddressId",
                schema: "USER",
                table: "PROFILES",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PROFILES_ProfileImageId",
                schema: "USER",
                table: "PROFILES",
                column: "ProfileImageId");

            migrationBuilder.CreateIndex(
                name: "IX_PROFILES_UserPreferenceFinanceId",
                schema: "USER",
                table: "PROFILES",
                column: "UserPreferenceFinanceId");

            migrationBuilder.CreateIndex(
                name: "IX_PROPOSITIONOFFERS_InvoiceId",
                schema: "DEAL",
                table: "PROPOSITIONOFFERS",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PROPOSITIONOFFERS_OfferId",
                schema: "DEAL",
                table: "PROPOSITIONOFFERS",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_PROPOSITIONOFFERS_OfferId1",
                schema: "DEAL",
                table: "PROPOSITIONOFFERS",
                column: "OfferId1");

            migrationBuilder.CreateIndex(
                name: "IX_PROPOSITIONREQUESTS_DocumentId",
                schema: "DEAL",
                table: "PROPOSITIONREQUESTS",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_PROPOSITIONREQUESTS_QuotationId",
                schema: "DEAL",
                table: "PROPOSITIONREQUESTS",
                column: "QuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_PROPOSITIONREQUESTS_RequestId",
                schema: "DEAL",
                table: "PROPOSITIONREQUESTS",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_PROPOSITIONREQUESTS_RequestId1",
                schema: "DEAL",
                table: "PROPOSITIONREQUESTS",
                column: "RequestId1");

            migrationBuilder.CreateIndex(
                name: "IX_PROPOSITIONS_OwnerId",
                schema: "DEAL",
                table: "PROPOSITIONS",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PROPOSITIONS_PropositionAmountId",
                schema: "DEAL",
                table: "PROPOSITIONS",
                column: "PropositionAmountId");

            migrationBuilder.CreateIndex(
                name: "IX_PROPOSITIONS_PropositionMessagesPostId",
                schema: "DEAL",
                table: "PROPOSITIONS",
                column: "PropositionMessagesPostId");

            migrationBuilder.CreateIndex(
                name: "IX_PROPOSITIONS_PropositionsFinanceOperationId",
                schema: "DEAL",
                table: "PROPOSITIONS",
                column: "PropositionsFinanceOperationId",
                unique: true,
                filter: "[PropositionsFinanceOperationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PROPOSITIONSFINANCEOPERATIONS_OwnerId",
                schema: "FINANCE",
                table: "PROPOSITIONSFINANCEOPERATIONS",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_QUOTATIONS_AddressId",
                schema: "ACCOUNTING",
                table: "QUOTATIONS",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_QUOTATIONS_ClientAddressId",
                schema: "ACCOUNTING",
                table: "QUOTATIONS",
                column: "ClientAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_QUOTATIONS_ClientSignatureId",
                schema: "ACCOUNTING",
                table: "QUOTATIONS",
                column: "ClientSignatureId");

            migrationBuilder.CreateIndex(
                name: "IX_QUOTATIONS_CompanyId",
                schema: "ACCOUNTING",
                table: "QUOTATIONS",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_QUOTATIONS_OwnerId",
                schema: "ACCOUNTING",
                table: "QUOTATIONS",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_QUOTATIONS_PostId",
                schema: "ACCOUNTING",
                table: "QUOTATIONS",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_QUOTATIONS_TermConditionPostId",
                schema: "ACCOUNTING",
                table: "QUOTATIONS",
                column: "TermConditionPostId");

            migrationBuilder.CreateIndex(
                name: "IX_QUOTATIONS_WarrantyPostId",
                schema: "ACCOUNTING",
                table: "QUOTATIONS",
                column: "WarrantyPostId");

            migrationBuilder.CreateIndex(
                name: "IX_REQUESTS_InvoiceId",
                schema: "DEAL",
                table: "REQUESTS",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SERVICEGATEWAY_OwnerId",
                schema: "FINANCE",
                table: "SERVICEGATEWAY",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_STOREDFILES_DealDocumentId",
                schema: "DOCUMENT",
                table: "STOREDFILES",
                column: "DealDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_STOREDFILES_OwnerId",
                schema: "DOCUMENT",
                table: "STOREDFILES",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TAGS_DealId",
                schema: "SEARCH",
                table: "TAGS",
                column: "DealId");

            migrationBuilder.CreateIndex(
                name: "IX_TAGS_OwnerId",
                schema: "SEARCH",
                table: "TAGS",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_UNITPRICINGS_CurrencyCodeId",
                schema: "PRICING",
                table: "UNITPRICINGS",
                column: "CurrencyCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_UNITPRICINGS_OwnerId",
                schema: "PRICING",
                table: "UNITPRICINGS",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_USEREARNINGSWALLET_AmountId",
                schema: "FINANCE",
                table: "USEREARNINGSWALLET",
                column: "AmountId");

            migrationBuilder.CreateIndex(
                name: "IX_USEREARNINGSWALLET_FinanceOperationId",
                schema: "FINANCE",
                table: "USEREARNINGSWALLET",
                column: "FinanceOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_USEREARNINGSWALLET_OwnerId",
                schema: "FINANCE",
                table: "USEREARNINGSWALLET",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_USERPREFERENCEFINANCE_OwnerId",
                schema: "FINANCE",
                table: "USERPREFERENCEFINANCE",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PROFILES_ProfileId",
                table: "AspNetUsers",
                column: "ProfileId",
                principalSchema: "USER",
                principalTable: "PROFILES",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CROSSINGPAYOUTS_DEALSFINANCEOPERATIONS_DealsFinanceOpertationId",
                schema: "FINANCE",
                table: "CROSSINGPAYOUTS",
                column: "DealsFinanceOpertationId",
                principalSchema: "FINANCE",
                principalTable: "DEALSFINANCEOPERATIONS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DEALS_DEALSFINANCEOPERATIONS_DealsFinanceOpertationId",
                schema: "DEAL",
                table: "DEALS",
                column: "DealsFinanceOpertationId",
                principalSchema: "FINANCE",
                principalTable: "DEALSFINANCEOPERATIONS",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ADDRESSES_LOCATIONS_LocationId",
                schema: "SHARED",
                table: "ADDRESSES");

            migrationBuilder.DropForeignKey(
                name: "FK_AMOUNTS_INVOICES_InvoiceId",
                schema: "PRICING",
                table: "AMOUNTS");

            migrationBuilder.DropForeignKey(
                name: "FK_AMOUNTS_QUOTATIONS_QuotationId",
                schema: "PRICING",
                table: "AMOUNTS");

            migrationBuilder.DropForeignKey(
                name: "FK_AMOUNTS_UNITPRICINGS_AmountUnitId",
                schema: "PRICING",
                table: "AMOUNTS");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PROFILES_ProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_FINANCEOPERATIONS_FINANCESUPPORTS_FinanceSupportId",
                schema: "FINANCE",
                table: "FINANCEOPERATIONS");

            migrationBuilder.DropForeignKey(
                name: "FK_DEALS_ADDRESSES_AddressId",
                schema: "DEAL",
                table: "DEALS");

            migrationBuilder.DropForeignKey(
                name: "FK_POSTINGDEALPAYMENTS_DEALS_DealId",
                schema: "FINNACE",
                table: "POSTINGDEALPAYMENTS");

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
                name: "AuditTrails");

            migrationBuilder.DropTable(
                name: "CROSSINGPAYMENTS",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "CROSSINGPAYOUTS",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "CYBERSORUCEPAYMENTSREQUEST",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "CYBERSORUCEPAYMENTSRESPONSE",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "CYBERSORUCEPAYOUTSREQUEST",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "CYBERSORUCEPAYOUTSRESPONSE",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "CYBERSOURCESERVICEGATEWAY",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "DEALFEESTIERS",
                schema: "FINNACE");

            migrationBuilder.DropTable(
                name: "DEALPAYMENTMANORS",
                schema: "PRICING");

            migrationBuilder.DropTable(
                name: "LOG",
                schema: "MONITORING");

            migrationBuilder.DropTable(
                name: "MESSAGES",
                schema: "COMMUNICATION");

            migrationBuilder.DropTable(
                name: "NPCOMPLETEARNINGSWALLET",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "PAYPALACCOUNT",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "POSTDEALFEESTIERS",
                schema: "FINNACE");

            migrationBuilder.DropTable(
                name: "PROPOSITIONOFFERS",
                schema: "DEAL");

            migrationBuilder.DropTable(
                name: "PROPOSITIONREQUESTS",
                schema: "DEAL");

            migrationBuilder.DropTable(
                name: "TAGS",
                schema: "SEARCH");

            migrationBuilder.DropTable(
                name: "USEREARNINGSWALLET",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CYBERSORUCEPAYMENTS",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "BANKACCOUNTS",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "CREDITCARDS",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "GateWayOperationRequests",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "CYBERSORUCEPAYOUTS",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "GateWayOperationResponses",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "FEESTIERS",
                schema: "FINNACE");

            migrationBuilder.DropTable(
                name: "OFFERS",
                schema: "DEAL");

            migrationBuilder.DropTable(
                name: "PROPOSITIONS",
                schema: "DEAL");

            migrationBuilder.DropTable(
                name: "REQUESTS",
                schema: "DEAL");

            migrationBuilder.DropTable(
                name: "GateWayOperationPayments",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "GateWayOperationPayouts",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "PROPOSITIONMESSAGESPOSTS",
                schema: "DOCUMENTS");

            migrationBuilder.DropTable(
                name: "PROPOSITIONSFINANCEOPERATIONS",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "LOCATIONS",
                schema: "SHARED");

            migrationBuilder.DropTable(
                name: "INVOICES",
                schema: "ACCOUNTING");

            migrationBuilder.DropTable(
                name: "QUOTATIONS",
                schema: "ACCOUNTING");

            migrationBuilder.DropTable(
                name: "COMPANY",
                schema: "SHARED");

            migrationBuilder.DropTable(
                name: "SIGNATURE",
                schema: "SHARED");

            migrationBuilder.DropTable(
                name: "UNITPRICINGS",
                schema: "PRICING");

            migrationBuilder.DropTable(
                name: "CURRENCIES",
                schema: "PRICING");

            migrationBuilder.DropTable(
                name: "PROFILES",
                schema: "USER");

            migrationBuilder.DropTable(
                name: "PROFILEIMAGES",
                schema: "USER");

            migrationBuilder.DropTable(
                name: "STOREDFILES",
                schema: "DOCUMENT");

            migrationBuilder.DropTable(
                name: "FINANCESUPPORTS",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "USERPREFERENCEFINANCE",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "ADDRESSES",
                schema: "SHARED");

            migrationBuilder.DropTable(
                name: "DEALS",
                schema: "DEAL");

            migrationBuilder.DropTable(
                name: "DEALDOCUMENTS",
                schema: "DEAL");

            migrationBuilder.DropTable(
                name: "DEALMESSAGESPOSTS",
                schema: "DOCUMENTS");

            migrationBuilder.DropTable(
                name: "DEALPRICEREFERENCES",
                schema: "PRICING");

            migrationBuilder.DropTable(
                name: "DEALSFINANCEOPERATIONS",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "MARKETS",
                schema: "MARKET");

            migrationBuilder.DropTable(
                name: "POSTS",
                schema: "SHARED");

            migrationBuilder.DropTable(
                name: "POSTINGDEALPAYMENTS",
                schema: "FINNACE");

            migrationBuilder.DropTable(
                name: "COLLECTION_MARKETS",
                schema: "MARKET");

            migrationBuilder.DropTable(
                name: "FINANCEOPERATIONS",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "AMOUNTS",
                schema: "PRICING");

            migrationBuilder.DropTable(
                name: "SERVICEGATEWAY",
                schema: "FINANCE");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
