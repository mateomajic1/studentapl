using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentApl.Migrations
{
    public partial class StudentPoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "bodovi",
                table: "students",
                newName: "points");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "points",
                table: "students",
                newName: "bodovi");
        }
    }
}
