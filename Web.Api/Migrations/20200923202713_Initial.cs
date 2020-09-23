using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    Street1 = table.Column<string>(nullable: false),
                    Street2 = table.Column<string>(nullable: true),
                    Street3 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    Zip = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    PhoneLand = table.Column<string>(nullable: true),
                    PhoneMobile = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    AddressType = table.Column<string>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.UniqueConstraint("AK_Addresses_EmployeeId_AddressType", x => new { x.EmployeeId, x.AddressType });
                });

            migrationBuilder.CreateTable(
                name: "EmergencyContacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: false),
                    RelationshipStatusId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyContacts", x => x.Id);
                    table.UniqueConstraint("AK_EmergencyContacts_EmployeeId_RelationshipStatusId", x => new { x.EmployeeId, x.RelationshipStatusId });
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    NameFirst = table.Column<string>(nullable: false),
                    NameMiddle = table.Column<string>(nullable: true),
                    NameLast = table.Column<string>(nullable: false),
                    NamePrintOnCheck = table.Column<string>(nullable: true),
                    SSN = table.Column<string>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    MaritalStatus = table.Column<string>(nullable: true),
                    Ethnicity = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    IsDisabled = table.Column<bool>(nullable: false),
                    DisabilityDesc = table.Column<string>(maxLength: 2000, nullable: true),
                    IsI9OnFile = table.Column<bool>(nullable: false),
                    ImmigrationId = table.Column<int>(nullable: false),
                    AddressHomeId = table.Column<int>(nullable: false),
                    AddressOtherId = table.Column<int>(nullable: false),
                    EmploymentInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    UserName = table.Column<string>(nullable: false),
                    RoleName = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.UniqueConstraint("AK_UserRoles_UserName", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "UserSessions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    UserName = table.Column<string>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    UserOID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    HireDate = table.Column<DateTime>(nullable: false),
                    OriginalHireDate = table.Column<DateTime>(nullable: false),
                    AdjustedServiceDate = table.Column<DateTime>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    EmploymentTypeId = table.Column<int>(nullable: false),
                    IsFullTime = table.Column<bool>(nullable: false),
                    IsSeasonal = table.Column<bool>(nullable: false),
                    IsExempt = table.Column<bool>(nullable: false),
                    IsKeyEmployee = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Supervisor = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LastDayWorked = table.Column<DateTime>(nullable: false),
                    LastDayOnBenefits = table.Column<DateTime>(nullable: false),
                    SeverancePaid = table.Column<bool>(nullable: false),
                    SeverancePayNotes = table.Column<string>(nullable: true),
                    TerminationDetail = table.Column<string>(nullable: true),
                    TerminationNotes = table.Column<string>(nullable: true),
                    IsReHireRecommended = table.Column<bool>(nullable: false),
                    ProtestUnemployment = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmploymentInfos_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Immigrations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    IsListA = table.Column<bool>(nullable: false),
                    ListADocTitle = table.Column<string>(nullable: true),
                    ListAIssuingAuthority = table.Column<string>(nullable: true),
                    ListADocNumber = table.Column<string>(nullable: true),
                    ListAExpiryDate = table.Column<DateTime>(nullable: false),
                    ListBDocTitle = table.Column<string>(nullable: true),
                    ListBIssuingAuthority = table.Column<string>(nullable: true),
                    ListBDocNumber = table.Column<string>(nullable: true),
                    ListBExpiryDate = table.Column<DateTime>(nullable: false),
                    ListCDocTitle = table.Column<string>(nullable: true),
                    ListCIssuingAuthority = table.Column<string>(nullable: true),
                    ListCDocNumber = table.Column<string>(nullable: true),
                    ListCExpiryDate = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Immigrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Immigrations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentInfos_EmployeeId",
                table: "EmploymentInfos",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Immigrations_EmployeeId",
                table: "Immigrations",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserName",
                table: "UserRoles",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_UserName",
                table: "UserSessions",
                column: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "EmergencyContacts");

            migrationBuilder.DropTable(
                name: "EmploymentInfos");

            migrationBuilder.DropTable(
                name: "Immigrations");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserSessions");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
