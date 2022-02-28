using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskerMaster.Data.Migrations
{
    public partial class Fixed_Names : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Workspaces_ScheduleId1",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Workspaces_Teams_TeamId",
                table: "Workspaces");

            migrationBuilder.DropIndex(
                name: "IX_Workspaces_TeamId",
                table: "Workspaces");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ScheduleId1",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ScheduleId1",
                table: "Teams");

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_TeamId",
                table: "Workspaces",
                column: "TeamId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Workspaces_Teams_TeamId",
                table: "Workspaces",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workspaces_Teams_TeamId",
                table: "Workspaces");

            migrationBuilder.DropIndex(
                name: "IX_Workspaces_TeamId",
                table: "Workspaces");

            migrationBuilder.AddColumn<string>(
                name: "ScheduleId1",
                table: "Teams",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_TeamId",
                table: "Workspaces",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ScheduleId1",
                table: "Teams",
                column: "ScheduleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Workspaces_ScheduleId1",
                table: "Teams",
                column: "ScheduleId1",
                principalTable: "Workspaces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workspaces_Teams_TeamId",
                table: "Workspaces",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
