using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerceSite.Migrations
{
    public partial class DatabaseFile5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Approve",
                table: "Post",
                maxLength: 4,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approve",
                table: "Post");
        }
    }
}
