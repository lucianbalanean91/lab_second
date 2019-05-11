using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projectTask.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Tasks",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Title = table.Column<string>(nullable: true),
            //        Description = table.Column<string>(nullable: true),
            //        Added = table.Column<DateTime>(nullable: false),
            //        Deadline = table.Column<DateTime>(nullable: false),
            //        Important = table.Column<int>(nullable: false),
            //        State = table.Column<int>(nullable: false),
            //        CloseAt = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tasks", x => x.Id);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
