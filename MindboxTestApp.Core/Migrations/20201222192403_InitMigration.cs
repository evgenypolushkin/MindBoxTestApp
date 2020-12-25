using Microsoft.EntityFrameworkCore.Migrations;

namespace MindboxTestApp.Core.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Ellipses",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MinorAxisLength = table.Column<double>(nullable: false),
                    MajorAxisLength = table.Column<double>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Ellipses", x => x.Id); });

            migrationBuilder.CreateTable(
                "Triangles",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ALength = table.Column<double>(nullable: false),
                    BLength = table.Column<double>(nullable: false),
                    ABAngle = table.Column<double>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Triangles", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ellipses");

            migrationBuilder.DropTable(
                name: "Triangles");
        }
    }
}