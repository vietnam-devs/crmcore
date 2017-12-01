using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CRMCore.DBMigration.Console.Data.Migrations.CRMCore
{
    public partial class AddTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "crm_Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssignedTo = table.Column<Guid>(nullable: false),
                    CategoryType = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    DueType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    TaskStatus = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_crm_Tasks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "crm_Tasks");
        }
    }
}
