using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EatvardDataAccessLibrary.Migrations
{
    public partial class JWTTokenAddedInUserAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JWTToken",
                table: "Users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JWTToken",
                table: "Users");
        }
    }
}
