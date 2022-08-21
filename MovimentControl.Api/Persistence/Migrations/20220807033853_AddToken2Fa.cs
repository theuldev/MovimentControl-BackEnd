using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovimentControl.Api.Persistence.Migrations
{
    public partial class AddToken2Fa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "tb_User",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "tb_User");
        }
    }
}
