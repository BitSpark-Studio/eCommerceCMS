using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerceSite.Migrations
{
    public partial class DatabaseFile2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confrim_Password",
                table: "Owner");

            migrationBuilder.CreateTable(
                name: "OwnerRegistrationViewModel",
                columns: table => new
                {
                    OID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_Name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Last_Name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Confrim_Password = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerRegistrationViewModel", x => x.OID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnerRegistrationViewModel");

            migrationBuilder.AddColumn<string>(
                name: "Confrim_Password",
                table: "Owner",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
