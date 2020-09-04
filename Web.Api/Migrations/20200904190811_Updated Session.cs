using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Api.Migrations
{
    public partial class UpdatedSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "SessionModels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserOID",
                table: "SessionModels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IPAddress",
                table: "SessionModels");

            migrationBuilder.DropColumn(
                name: "UserOID",
                table: "SessionModels");
        }
    }
}
