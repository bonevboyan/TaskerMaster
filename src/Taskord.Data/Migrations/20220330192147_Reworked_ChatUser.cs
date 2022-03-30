using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskord.Data.Migrations
{
    public partial class Reworked_ChatUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "ChatUsers");

            migrationBuilder.AddColumn<string>(
                name: "LastReadMessageId",
                table: "ChatUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastReadMessageId",
                table: "ChatUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "ChatUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
