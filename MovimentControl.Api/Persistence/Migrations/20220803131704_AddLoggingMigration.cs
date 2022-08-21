using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovimentControl.Api.Persistence.Migrations
{
    public partial class AddLoggingMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LoggedTime",
                table: "tb_User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoggedTime",
                table: "tb_User");
        }
    }
}
