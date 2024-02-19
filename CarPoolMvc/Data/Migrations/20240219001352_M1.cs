using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarPoolMvc.Data.Migrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Mobile = table.Column<string>(type: "TEXT", nullable: true),
                    Street = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    PostalCode = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Modified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
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
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
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
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
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
                name: "Manifest",
                columns: table => new
                {
                    ManifestId = table.Column<int>(type: "INTEGER", nullable: false),
                    MemberId = table.Column<int>(type: "INTEGER", nullable: false),
                    TripId = table.Column<int>(type: "INTEGER", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Modified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manifest", x => new { x.ManifestId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_Manifest_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    Make = table.Column<string>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "INTEGER", nullable: true),
                    VehicleType = table.Column<string>(type: "TEXT", nullable: true),
                    MemberId = table.Column<int>(type: "INTEGER", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Modified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_Vehicle_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "MemberId");
                });

            migrationBuilder.CreateTable(
                name: "Trip",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "INTEGER", nullable: false),
                    VehicleId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Time = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    Destination = table.Column<string>(type: "TEXT", nullable: true),
                    MeetingAddress = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Modified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => new { x.TripId, x.VehicleId });
                    table.ForeignKey(
                        name: "FK_Trip_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicle",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "50fe6afd-daa7-4eca-bc3d-a4e42dd176cf", null, "Owner Role", "Role", "Owner", "OWNER" },
                    { "8d20ebd0-bdac-4efd-bf6b-b57d5d8a6d24", null, "Passenger Role", "Role", "Passenger", "PASSENGER" },
                    { "ed665b0b-5a76-4ecc-85c0-1642a86ed0d7", null, "Administrator Role", "Role", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "230a820c-fb7a-4808-ab92-bbb825fe67e2", 0, "48bc7ad9-33ac-4604-8b3e-3406e5515c1a", "User", "a@a.a", true, "Admin", "Admin", false, null, null, "A@A.A", "AQAAAAIAAYagAAAAEEdWwUZMrbiO8hQsJadE6JfFPux5o72ZYAMrHVdQwRjSQf+NzaVdDX85WMbWWMpLxQ==", null, false, "5ac32923-1fbd-49c5-bad5-3a5d988ace4d", false, "a@a.a" },
                    { "2a0b778f-57dc-4d70-a804-7f0f0a713af7", 0, "e3beb9a9-0d97-442d-9e4d-581dd20849c7", "User", "o@o.o", true, "Owner", "Owner", false, null, null, "O@O.O", "AQAAAAIAAYagAAAAEIgTmOwLu9v5MpknbvV28ETBkFbT+N8tSAoH8wCdEn116YNLAISBO2E0ijwbShoMVA==", null, false, "93d12b47-2e33-4390-aa0d-f33326f434aa", false, "o@o.o" },
                    { "452363e3-6567-4a4b-913d-9421274c5275", 0, "d861f493-75f6-4c85-a02e-b9a54c384bbf", "User", "p@p.p", true, "Passenger", "Passenger", false, null, null, "P@P.P", "AQAAAAIAAYagAAAAEF2HHxSR9x2pMT3LVU6PqfjvSwLwgTE6N+ObjGrX6+NN1nrHtwzjFT7GvvPJ4wvceg==", null, false, "a26aab93-f311-460a-94c3-92612dddbe72", false, "p@p.p" }
                });

            migrationBuilder.InsertData(
                table: "Member",
                columns: new[] { "MemberId", "City", "Country", "Created", "CreatedBy", "Email", "FirstName", "LastName", "Mobile", "Modified", "ModifiedBy", "PostalCode", "Street" },
                values: new object[,]
                {
                    { 1, "Richmond", "Canada", new DateTime(2024, 2, 18, 16, 13, 52, 151, DateTimeKind.Local).AddTicks(350), "System", "sam@fox.com", "Sam", "Fox", "778-111-2222", new DateTime(2024, 2, 18, 16, 13, 52, 151, DateTimeKind.Local).AddTicks(390), "System", "V4F 1M7", "457 Fox Avenue" },
                    { 2, "Delta", "Canada", new DateTime(2024, 2, 18, 16, 13, 52, 151, DateTimeKind.Local).AddTicks(420), "System", "ann@day.com", "Ann", "Day", "604-333-6666", new DateTime(2024, 2, 18, 16, 13, 52, 151, DateTimeKind.Local).AddTicks(420), "System", "V6G 1M6", "231 Reiver Road" },
                    { 3, "Delta", "Canada", new DateTime(2024, 2, 18, 16, 13, 52, 151, DateTimeKind.Local).AddTicks(420), "System", "lucas@jian.com", "Lucas", "Jian", "604-333-6666", new DateTime(2024, 2, 18, 16, 13, 52, 151, DateTimeKind.Local).AddTicks(420), "System", "V6G 1M6", "231 Reiver Road" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "ed665b0b-5a76-4ecc-85c0-1642a86ed0d7", "230a820c-fb7a-4808-ab92-bbb825fe67e2" },
                    { "50fe6afd-daa7-4eca-bc3d-a4e42dd176cf", "2a0b778f-57dc-4d70-a804-7f0f0a713af7" },
                    { "8d20ebd0-bdac-4efd-bf6b-b57d5d8a6d24", "452363e3-6567-4a4b-913d-9421274c5275" }
                });

            migrationBuilder.InsertData(
                table: "Manifest",
                columns: new[] { "ManifestId", "MemberId", "Created", "CreatedBy", "Modified", "ModifiedBy", "Notes", "TripId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 2, 18, 16, 13, 52, 151, DateTimeKind.Local).AddTicks(710), "System", new DateTime(2024, 2, 18, 16, 13, 52, 151, DateTimeKind.Local).AddTicks(710), "System", "I will be driving to work", 1 },
                    { 2, 2, new DateTime(2024, 2, 18, 16, 13, 52, 151, DateTimeKind.Local).AddTicks(720), "System", new DateTime(2024, 2, 18, 16, 13, 52, 151, DateTimeKind.Local).AddTicks(720), "System", "I will be driving to work", 2 },
                    { 3, 3, new DateTime(2024, 2, 18, 16, 13, 52, 151, DateTimeKind.Local).AddTicks(720), "System", new DateTime(2024, 2, 18, 16, 13, 52, 151, DateTimeKind.Local).AddTicks(720), "System", "I will be driving to work", 3 }
                });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "VehicleId", "Created", "CreatedBy", "Make", "MemberId", "Model", "Modified", "ModifiedBy", "NumberOfSeats", "VehicleType", "Year" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 18, 16, 13, 52, 151, DateTimeKind.Local).AddTicks(440), "System", "Ford", 1, "Escort", new DateTime(2024, 2, 18, 16, 13, 52, 151, DateTimeKind.Local).AddTicks(440), "System", 5, "Sedan", 2020 },
                    { 2, new DateTime(2024, 2, 18, 16, 13, 52, 151, DateTimeKind.Local).AddTicks(450), "System", "Kia", 2, "Soul", new DateTime(2024, 2, 18, 16, 13, 52, 151, DateTimeKind.Local).AddTicks(450), "System", 5, "Compact", 2022 },
                    { 3, new DateTime(2024, 2, 18, 16, 13, 52, 151, DateTimeKind.Local).AddTicks(450), "System", "Honda", 3, "Odyssey", new DateTime(2024, 2, 18, 16, 13, 52, 151, DateTimeKind.Local).AddTicks(450), "System", 8, "Minivan", 2019 }
                });

            migrationBuilder.InsertData(
                table: "Trip",
                columns: new[] { "TripId", "VehicleId", "Created", "CreatedBy", "Date", "Destination", "MeetingAddress", "Modified", "ModifiedBy", "Time" },
                values: new object[,]
                {
                    { 1, 1, null, null, new DateOnly(2024, 2, 2), "123 Marine Drive, Burnaby", "1123 River Road, Coquitlam", null, null, new TimeOnly(12, 0, 0) },
                    { 2, 2, null, null, new DateOnly(2024, 2, 3), "231 Boundary Road, Vancouver", "345 King George Highway, Surrey", null, null, new TimeOnly(8, 0, 0) },
                    { 3, 3, null, null, new DateOnly(2024, 2, 4), "12345 Lougheed Highway, Coquitlam", "540 Oliver Road, Richmond", null, null, new TimeOnly(15, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Manifest_MemberId",
                table: "Manifest",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_VehicleId",
                table: "Trip",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_MemberId",
                table: "Vehicle",
                column: "MemberId");
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
                name: "Manifest");

            migrationBuilder.DropTable(
                name: "Trip");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "Member");
        }
    }
}
