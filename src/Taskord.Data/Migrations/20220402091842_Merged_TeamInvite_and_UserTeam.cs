using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskord.Data.Migrations
{
    public partial class Merged_TeamInvite_and_UserTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTeams_AspNetUsers_UserId",
                table: "UserTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTeams_Teams_TeamId",
                table: "UserTeams");

            migrationBuilder.DropTable(
                name: "TeamInvites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTeams",
                table: "UserTeams");

            migrationBuilder.AlterColumn<string>(
                name: "TeamId",
                table: "UserTeams",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserTeams",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UserTeams",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UserTeams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "UserTeams",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InviterId",
                table: "UserTeams",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserTeams",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "UserTeams",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "UserTeams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTeams",
                table: "UserTeams",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeams_InviterId",
                table: "UserTeams",
                column: "InviterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeams_UserId",
                table: "UserTeams",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTeams_AspNetUsers_InviterId",
                table: "UserTeams",
                column: "InviterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTeams_AspNetUsers_UserId",
                table: "UserTeams",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTeams_Teams_TeamId",
                table: "UserTeams",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTeams_AspNetUsers_InviterId",
                table: "UserTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTeams_AspNetUsers_UserId",
                table: "UserTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTeams_Teams_TeamId",
                table: "UserTeams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTeams",
                table: "UserTeams");

            migrationBuilder.DropIndex(
                name: "IX_UserTeams_InviterId",
                table: "UserTeams");

            migrationBuilder.DropIndex(
                name: "IX_UserTeams_UserId",
                table: "UserTeams");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserTeams");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UserTeams");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "UserTeams");

            migrationBuilder.DropColumn(
                name: "InviterId",
                table: "UserTeams");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserTeams");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "UserTeams");

            migrationBuilder.DropColumn(
                name: "State",
                table: "UserTeams");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserTeams",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TeamId",
                table: "UserTeams",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTeams",
                table: "UserTeams",
                columns: new[] { "UserId", "TeamId" });

            migrationBuilder.CreateTable(
                name: "TeamInvites",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TeamId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamInvites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamInvites_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeamInvites_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeamInvites_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamInvites_ReceiverId",
                table: "TeamInvites",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamInvites_SenderId",
                table: "TeamInvites",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamInvites_TeamId",
                table: "TeamInvites",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTeams_AspNetUsers_UserId",
                table: "UserTeams",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTeams_Teams_TeamId",
                table: "UserTeams",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
