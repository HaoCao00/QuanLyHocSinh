using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyHocSinh.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CMND",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePfIssue",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PlaceOfIssue",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CMND",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DatePfIssue",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PlaceOfIssue",
                table: "Students");
        }
    }
}
