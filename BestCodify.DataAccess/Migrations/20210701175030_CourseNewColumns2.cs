using Microsoft.EntityFrameworkCore.Migrations;

namespace BestCodify.DataAccess.Migrations
{
    public partial class CourseNewColumns2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Occupancy",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "SqFt",
                table: "Courses",
                newName: "SubTitle");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "SubTitle",
                table: "Courses",
                newName: "SqFt");

            migrationBuilder.AddColumn<int>(
                name: "Occupancy",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
