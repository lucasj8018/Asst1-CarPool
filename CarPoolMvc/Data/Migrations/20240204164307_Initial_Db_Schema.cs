using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarPoolMvc.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Db_Schema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "Member",
                columns: new[] { "MemberId", "City", "Country", "Created", "CreatedBy", "Email", "FirstName", "LastName", "Mobile", "Modified", "ModifiedBy", "PostalCode", "Street" },
                values: new object[,]
                {
                    { 1, "Richmond", "Canada", new DateTime(2024, 2, 4, 8, 43, 4, 813, DateTimeKind.Local).AddTicks(1061), "System", "sam@fox.com", "Sam", "Fox", "778-111-2222", new DateTime(2024, 2, 4, 8, 43, 4, 813, DateTimeKind.Local).AddTicks(1103), "System", "V4F 1M7", "457 Fox Avenue" },
                    { 2, "Delta", "Canada", new DateTime(2024, 2, 4, 8, 43, 4, 813, DateTimeKind.Local).AddTicks(1113), "System", "ann@day.com", "Ann", "Day", "604-333-6666", new DateTime(2024, 2, 4, 8, 43, 4, 813, DateTimeKind.Local).AddTicks(1114), "System", "V6G 1M6", "231 Reiver Road" },
                    { 3, "Delta", "Canada", new DateTime(2024, 2, 4, 8, 43, 4, 813, DateTimeKind.Local).AddTicks(1118), "System", "lucas@jian.com", "Lucas", "Jian", "604-333-6666", new DateTime(2024, 2, 4, 8, 43, 4, 813, DateTimeKind.Local).AddTicks(1119), "System", "V6G 1M6", "231 Reiver Road" }
                });

            migrationBuilder.InsertData(
                table: "Manifest",
                columns: new[] { "ManifestId", "MemberId", "Created", "CreatedBy", "Modified", "ModifiedBy", "Notes", "TripId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 2, 4, 8, 43, 4, 813, DateTimeKind.Local).AddTicks(1448), "System", new DateTime(2024, 2, 4, 8, 43, 4, 813, DateTimeKind.Local).AddTicks(1450), "System", "I will be driving to work", 1 },
                    { 2, 2, new DateTime(2024, 2, 4, 8, 43, 4, 813, DateTimeKind.Local).AddTicks(1455), "System", new DateTime(2024, 2, 4, 8, 43, 4, 813, DateTimeKind.Local).AddTicks(1457), "System", "I will be driving to work", 2 },
                    { 3, 3, new DateTime(2024, 2, 4, 8, 43, 4, 813, DateTimeKind.Local).AddTicks(1459), "System", new DateTime(2024, 2, 4, 8, 43, 4, 813, DateTimeKind.Local).AddTicks(1460), "System", "I will be driving to work", 3 }
                });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "VehicleId", "Created", "CreatedBy", "Make", "MemberId", "Model", "Modified", "ModifiedBy", "NumberOfSeats", "VehicleType", "Year" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 4, 8, 43, 4, 813, DateTimeKind.Local).AddTicks(1162), "System", "Ford", 1, "Escort", new DateTime(2024, 2, 4, 8, 43, 4, 813, DateTimeKind.Local).AddTicks(1164), "System", 5, "Sedan", 2020 },
                    { 2, new DateTime(2024, 2, 4, 8, 43, 4, 813, DateTimeKind.Local).AddTicks(1175), "System", "Kia", 2, "Soul", new DateTime(2024, 2, 4, 8, 43, 4, 813, DateTimeKind.Local).AddTicks(1176), "System", 5, "Compact", 2022 },
                    { 3, new DateTime(2024, 2, 4, 8, 43, 4, 813, DateTimeKind.Local).AddTicks(1179), "System", "Honda", 3, "Odyssey", new DateTime(2024, 2, 4, 8, 43, 4, 813, DateTimeKind.Local).AddTicks(1180), "System", 8, "Minivan", 2019 }
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
                name: "Manifest");

            migrationBuilder.DropTable(
                name: "Trip");

            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "Member");
        }
    }
}
