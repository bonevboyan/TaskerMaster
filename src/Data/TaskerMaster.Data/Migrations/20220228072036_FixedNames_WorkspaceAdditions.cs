using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskerMaster.Data.Migrations
{
    public partial class FixedNames_WorkspaceAdditions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buckets_Workspaces_WorkspaceId",
                table: "Buckets");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Companies_CompanyId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Workspaces_WorkspaceId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Workspaces_WorkspaceId1",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CompanyId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_WorkspaceId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "WorkspaceId1",
                table: "Teams",
                newName: "ScheduleId1");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_WorkspaceId1",
                table: "Teams",
                newName: "IX_Teams_ScheduleId1");

            migrationBuilder.RenameColumn(
                name: "WorkspaceId",
                table: "Buckets",
                newName: "ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Buckets_WorkspaceId",
                table: "Buckets",
                newName: "IX_Buckets_ScheduleId");

            migrationBuilder.AddColumn<string>(
                name: "ScheduleId",
                table: "Teams",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Companies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkspaceId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ScheduleId",
                table: "Teams",
                column: "ScheduleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_WorkspaceId",
                table: "Teams",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_OwnerId",
                table: "Companies",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WorkspaceId",
                table: "AspNetUsers",
                column: "WorkspaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_WorkspaceId",
                table: "AspNetUsers",
                column: "WorkspaceId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Buckets_Workspaces_ScheduleId",
                table: "Buckets",
                column: "ScheduleId",
                principalTable: "Workspaces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_OwnerId",
                table: "Companies",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Companies_WorkspaceId",
                table: "Teams",
                column: "WorkspaceId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Workspaces_ScheduleId",
                table: "Teams",
                column: "ScheduleId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Workspaces_ScheduleId1",
                table: "Teams",
                column: "ScheduleId1",
                principalTable: "Workspaces",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_WorkspaceId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Buckets_Workspaces_ScheduleId",
                table: "Buckets");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_OwnerId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Companies_WorkspaceId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Workspaces_ScheduleId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Workspaces_ScheduleId1",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ScheduleId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_WorkspaceId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Companies_OwnerId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WorkspaceId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ScheduleId1",
                table: "Teams",
                newName: "WorkspaceId1");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_ScheduleId1",
                table: "Teams",
                newName: "IX_Teams_WorkspaceId1");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "Buckets",
                newName: "WorkspaceId");

            migrationBuilder.RenameIndex(
                name: "IX_Buckets_ScheduleId",
                table: "Buckets",
                newName: "IX_Buckets_WorkspaceId");

            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "Teams",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CompanyId",
                table: "Teams",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_WorkspaceId",
                table: "Teams",
                column: "WorkspaceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Buckets_Workspaces_WorkspaceId",
                table: "Buckets",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Companies_CompanyId",
                table: "Teams",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Workspaces_WorkspaceId",
                table: "Teams",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Workspaces_WorkspaceId1",
                table: "Teams",
                column: "WorkspaceId1",
                principalTable: "Workspaces",
                principalColumn: "Id");
        }
    }
}
