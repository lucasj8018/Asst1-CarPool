using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarPoolMvc.Data.Migrations
{
    /// <inheritdoc />
    public partial class M2 : Migration
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
                    ManifestId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MemberId = table.Column<int>(type: "INTEGER", nullable: false),
                    TripId = table.Column<int>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Modified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manifest", x => x.ManifestId);
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
                    TripId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                    table.PrimaryKey("PK_Trip", x => x.TripId);
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
                    { "24d5cc5b-c172-49ab-9ce6-e855b5e29f96", null, "Owner Role", "Role", "Owner", "OWNER" },
                    { "28fc9e1e-4e36-472a-aa76-c12647246d06", null, "Passenger Role", "Role", "Passenger", "PASSENGER" },
                    { "dc61d326-9b26-404b-9553-86ef2464d964", null, "Administrator Role", "Role", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "34919c6b-d52e-4b42-98f4-36a538fd8780", 0, "5a8fa9dd-4377-4f0a-8de3-695f6ffb99bc", "User", "p@p.p", true, "Passenger", "Passenger", false, null, null, "P@P.P", "AQAAAAIAAYagAAAAEIbn35deIKBruk03bWo2fhq5mcdHZpOeU7yyUZCGDLOmYiLe632mvMx7RbOTUDsUcQ==", null, false, "be6a4035-033e-4bb5-9ad0-3c654aa2abf2", false, "p@p.p" },
                    { "4c9b9702-90ef-4c70-bea9-d1a8ba743b2e", 0, "f1192ecf-bf23-42a9-8220-b3c90bbdf82e", "User", "a@a.a", true, "Admin", "Admin", false, null, null, "A@A.A", "AQAAAAIAAYagAAAAEA6qIvm4YFKDub70pLA9dO+x1OBzIa153ulVWmYkeP+rWZdYdiygjsggAZsG3qAjsg==", null, false, "23c3d607-e117-4d51-95bf-c9b57944afc6", false, "a@a.a" },
                    { "57f33989-0a56-441e-a8ac-29ce3fba3d3f", 0, "f48b86fb-165a-4dae-94f0-071aea6e3150", "User", "o@o.o", true, "Owner", "Owner", false, null, null, "O@O.O", "AQAAAAIAAYagAAAAEHerNhGXQ7QCfnKukimbWaoWz5D4lGIH5xxryP6SWFNwU8CL9s9c7I6eREH+0rNk1Q==", null, false, "23687571-d8a5-4a84-be04-c19e70169f8f", false, "o@o.o" }
                });

            migrationBuilder.InsertData(
                table: "Member",
                columns: new[] { "MemberId", "City", "Country", "Created", "CreatedBy", "Email", "FirstName", "LastName", "Mobile", "Modified", "ModifiedBy", "PostalCode", "Street" },
                values: new object[,]
                {
                    { 1, "Richmond", "Canada", new DateTime(2024, 2, 18, 21, 0, 7, 563, DateTimeKind.Local).AddTicks(7680), "System", "sam@fox.com", "Sam", "Fox", "778-111-2222", new DateTime(2024, 2, 18, 21, 0, 7, 563, DateTimeKind.Local).AddTicks(7760), "System", "V4F 1M7", "457 Fox Avenue" },
                    { 2, "Delta", "Canada", new DateTime(2024, 2, 18, 21, 0, 7, 563, DateTimeKind.Local).AddTicks(7770), "System", "ann@day.com", "Ann", "Day", "604-333-6666", new DateTime(2024, 2, 18, 21, 0, 7, 563, DateTimeKind.Local).AddTicks(7770), "System", "V6G 1M6", "231 Reiver Road" },
                    { 3, "Delta", "Canada", new DateTime(2024, 2, 18, 21, 0, 7, 563, DateTimeKind.Local).AddTicks(7770), "System", "lucas@jian.com", "Lucas", "Jian", "604-333-6666", new DateTime(2024, 2, 18, 21, 0, 7, 563, DateTimeKind.Local).AddTicks(7770), "System", "V6G 1M6", "231 Reiver Road" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "28fc9e1e-4e36-472a-aa76-c12647246d06", "34919c6b-d52e-4b42-98f4-36a538fd8780" },
                    { "dc61d326-9b26-404b-9553-86ef2464d964", "4c9b9702-90ef-4c70-bea9-d1a8ba743b2e" },
                    { "24d5cc5b-c172-49ab-9ce6-e855b5e29f96", "57f33989-0a56-441e-a8ac-29ce3fba3d3f" }
                });

            migrationBuilder.InsertData(
                table: "Manifest",
                columns: new[] { "ManifestId", "Created", "CreatedBy", "MemberId", "Modified", "ModifiedBy", "Notes", "TripId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 18, 21, 0, 7, 563, DateTimeKind.Local).AddTicks(8050), "System", 1, new DateTime(2024, 2, 18, 21, 0, 7, 563, DateTimeKind.Local).AddTicks(8050), "System", "I will be driving to work", 1 },
                    { 2, new DateTime(2024, 2, 18, 21, 0, 7, 563, DateTimeKind.Local).AddTicks(8070), "System", 2, new DateTime(2024, 2, 18, 21, 0, 7, 563, DateTimeKind.Local).AddTicks(8070), "System", "I will be driving to work", 2 },
                    { 3, new DateTime(2024, 2, 18, 21, 0, 7, 563, DateTimeKind.Local).AddTicks(8070), "System", 3, new DateTime(2024, 2, 18, 21, 0, 7, 563, DateTimeKind.Local).AddTicks(8070), "System", "I will be driving to work", 3 }
                });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "VehicleId", "Created", "CreatedBy", "Make", "MemberId", "Model", "Modified", "ModifiedBy", "NumberOfSeats", "VehicleType", "Year" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 18, 21, 0, 7, 563, DateTimeKind.Local).AddTicks(7810), "System", "Ford", 1, "Escort", new DateTime(2024, 2, 18, 21, 0, 7, 563, DateTimeKind.Local).AddTicks(7810), "System", 5, "Sedan", 2020 },
                    { 2, new DateTime(2024, 2, 18, 21, 0, 7, 563, DateTimeKind.Local).AddTicks(7820), "System", "Kia", 2, "Soul", new DateTime(2024, 2, 18, 21, 0, 7, 563, DateTimeKind.Local).AddTicks(7820), "System", 5, "Compact", 2022 },
                    { 3, new DateTime(2024, 2, 18, 21, 0, 7, 563, DateTimeKind.Local).AddTicks(7820), "System", "Honda", 3, "Odyssey", new DateTime(2024, 2, 18, 21, 0, 7, 563, DateTimeKind.Local).AddTicks(7830), "System", 8, "Minivan", 2019 }
                });

            migrationBuilder.InsertData(
                table: "Trip",
                columns: new[] { "TripId", "Created", "CreatedBy", "Date", "Destination", "MeetingAddress", "Modified", "ModifiedBy", "Time", "VehicleId" },
                values: new object[,]
                {
                    { 1, null, null, new DateOnly(2024, 2, 2), "123 Marine Drive, Burnaby", "1123 River Road, Coquitlam", null, null, new TimeOnly(12, 0, 0), 1 },
                    { 2, null, null, new DateOnly(2024, 2, 3), "231 Boundary Road, Vancouver", "345 King George Highway, Surrey", null, null, new TimeOnly(8, 0, 0), 2 },
                    { 3, null, null, new DateOnly(2024, 2, 4), "12345 Lougheed Highway, Coquitlam", "540 Oliver Road, Richmond", null, null, new TimeOnly(15, 0, 0), 3 }
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
                name: "IX_Manifest_ManifestId_MemberId",
                table: "Manifest",
                columns: new[] { "ManifestId", "MemberId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Manifest_MemberId",
                table: "Manifest",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_TripId_VehicleId",
                table: "Trip",
                columns: new[] { "TripId", "VehicleId" },
                unique: true);

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
