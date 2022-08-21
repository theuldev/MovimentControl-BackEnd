using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovimentControl.Api.Persistence.Migrations
{
    public partial class AddEmailInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "tb_User",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "tb_User");
        }
    }
}
