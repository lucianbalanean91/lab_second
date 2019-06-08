using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace lab2_restapi_1205_taskmgmt.Migrations
{
    public partial class AddDateRegisterForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateRegister",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateRegister",
                table: "Users");
        }
    }
}
