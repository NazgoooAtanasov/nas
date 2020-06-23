using Microsoft.EntityFrameworkCore.Migrations;

namespace Nas.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uris",
                columns: table => new
                {
                    Slug = table.Column<string>(nullable: false),
                    Link = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uris", x => x.Slug);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uris");
        }
    }
}
