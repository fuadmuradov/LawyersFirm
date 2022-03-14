using Microsoft.EntityFrameworkCore.Migrations;

namespace LawyersFirm.Migrations
{
    public partial class addImageBlogWriter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "BlogWriters",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "BlogWriters");
        }
    }
}
