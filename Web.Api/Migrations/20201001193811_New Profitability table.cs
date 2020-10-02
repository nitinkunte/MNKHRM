using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Api.Migrations
{
    public partial class NewProfitabilitytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfitabilityId",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Profitability",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    PayRate = table.Column<double>(nullable: false),
                    PayRatePer = table.Column<string>(nullable: false),
                    PayRateHourly = table.Column<double>(nullable: false),
                    PayRateYearly = table.Column<double>(nullable: false),
                    IncomeRate = table.Column<double>(nullable: false),
                    IncomeRatePer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profitability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profitability_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profitability_EmployeeId",
                table: "Profitability",
                column: "EmployeeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profitability");

            migrationBuilder.DropColumn(
                name: "ProfitabilityId",
                table: "Employees");
        }
    }
}
