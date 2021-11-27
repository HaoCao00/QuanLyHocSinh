using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyHocSinh.Migrations
{
    public partial class AddImageComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgSources",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgSources",
                table: "Comments");
        }
    }
}
