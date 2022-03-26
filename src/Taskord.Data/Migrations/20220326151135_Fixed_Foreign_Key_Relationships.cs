using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskord.Data.Migrations
{
    public partial class Fixed_Foreign_Key_Relationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_AspNetUsers_ReceiverId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_AspNetUsers_SenderId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Chats_ChatId",
                table: "FriendRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests");

            migrationBuilder.RenameTable(
                name: "FriendRequests",
                newName: "Friendships");

            migrationBuilder.RenameIndex(
                name: "IX_FriendRequests_SenderId",
                table: "Friendships",
                newName: "IX_Friendships_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_FriendRequests_ReceiverId",
                table: "Friendships",
                newName: "IX_Friendships_ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_FriendRequests_ChatId",
                table: "Friendships",
                newName: "IX_Friendships_ChatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_ReceiverId",
                table: "Friendships",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_SenderId",
                table: "Friendships",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Chats_ChatId",
                table: "Friendships",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_ReceiverId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_SenderId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Chats_ChatId",
                table: "Friendships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships");

            migrationBuilder.RenameTable(
                name: "Friendships",
                newName: "FriendRequests");

            migrationBuilder.RenameIndex(
                name: "IX_Friendships_SenderId",
                table: "FriendRequests",
                newName: "IX_FriendRequests_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendships_ReceiverId",
                table: "FriendRequests",
                newName: "IX_FriendRequests_ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendships_ChatId",
                table: "FriendRequests",
                newName: "IX_FriendRequests_ChatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_AspNetUsers_ReceiverId",
                table: "FriendRequests",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_AspNetUsers_SenderId",
                table: "FriendRequests",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Chats_ChatId",
                table: "FriendRequests",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id");
        }
    }
}
