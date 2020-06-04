using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerceSite.Migrations
{
    public partial class DatabaseFile1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cetagorie",
                columns: table => new
                {
                    CID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Photo = table.Column<int>(maxLength: 100, nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    Details = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cetagorie", x => x.CID);
                });

            migrationBuilder.CreateTable(
                name: "Owner",
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
                    Confrim_Password = table.Column<string>(maxLength: 50, nullable: false),
                    JWT_Token = table.Column<string>(maxLength: 1000, nullable: false),
                    Photo = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.OID);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    PID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    Details = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PID);
                });

            migrationBuilder.CreateTable(
                name: "Cetagorie_Owner_Post",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PID = table.Column<int>(nullable: false),
                    OID = table.Column<int>(nullable: false),
                    CID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cetagorie_Owner_Post", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cetagorie_Owner_Post_Cetagorie_CID",
                        column: x => x.CID,
                        principalTable: "Cetagorie",
                        principalColumn: "CID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cetagorie_Owner_Post_Owner_OID",
                        column: x => x.OID,
                        principalTable: "Owner",
                        principalColumn: "OID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cetagorie_Owner_Post_Post_PID",
                        column: x => x.PID,
                        principalTable: "Post",
                        principalColumn: "PID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotoGellaries",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PID = table.Column<int>(nullable: false),
                    Picture_Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoGellaries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PhotoGellaries_Post_PID",
                        column: x => x.PID,
                        principalTable: "Post",
                        principalColumn: "PID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cetagorie_Owner_Post_CID",
                table: "Cetagorie_Owner_Post",
                column: "CID");

            migrationBuilder.CreateIndex(
                name: "IX_Cetagorie_Owner_Post_OID",
                table: "Cetagorie_Owner_Post",
                column: "OID");

            migrationBuilder.CreateIndex(
                name: "IX_Cetagorie_Owner_Post_PID",
                table: "Cetagorie_Owner_Post",
                column: "PID");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoGellaries_PID",
                table: "PhotoGellaries",
                column: "PID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cetagorie_Owner_Post");

            migrationBuilder.DropTable(
                name: "PhotoGellaries");

            migrationBuilder.DropTable(
                name: "Cetagorie");

            migrationBuilder.DropTable(
                name: "Owner");

            migrationBuilder.DropTable(
                name: "Post");
        }
    }
}
