using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EatvardDataAccessLibrary.Migrations
{
    public partial class AddressStreetNumberAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StreetNumber",
                table: "Address",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StreetNumber",
                table: "Address");
        }
    }
}
