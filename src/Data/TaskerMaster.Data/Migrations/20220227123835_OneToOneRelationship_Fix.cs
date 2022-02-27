using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskerMaster.Data.Migrations
{
    public partial class OneToOneRelationship_Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teams_WorkspaceId",
                table: "Teams");

            migrationBuilder.AddColumn<string>(
                name: "WorkspaceId1",
                table: "Teams",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_WorkspaceId",
                table: "Teams",
                column: "WorkspaceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_WorkspaceId1",
                table: "Teams",
                column: "WorkspaceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Workspaces_WorkspaceId1",
                table: "Teams",
                column: "WorkspaceId1",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Workspaces_WorkspaceId1",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_WorkspaceId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_WorkspaceId1",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "WorkspaceId1",
                table: "Teams");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_WorkspaceId",
                table: "Teams",
                column: "WorkspaceId");
        }
    }
}
