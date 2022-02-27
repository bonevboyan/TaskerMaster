using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskerMaster.Data.Migrations
{
    public partial class ForeignRelationships_Fixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Tasks_TaskId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_ApplicationUserId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "TagTask");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ApplicationUserId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TaskId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "TeamId",
                table: "Discussions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    BucketId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Buckets_BucketId",
                        column: x => x.BucketId,
                        principalTable: "Buckets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserCard",
                columns: table => new
                {
                    CardsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserCard", x => new { x.CardsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserCard_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserCard_Cards_CardsId",
                        column: x => x.CardsId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CardTag",
                columns: table => new
                {
                    CardsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TagsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTag", x => new { x.CardsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_CardTag_Cards_CardsId",
                        column: x => x.CardsId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserCard_UsersId",
                table: "ApplicationUserCard",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_BucketId",
                table: "Cards",
                column: "BucketId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_IsDeleted",
                table: "Cards",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CardTag_TagsId",
                table: "CardTag",
                column: "TagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserCard");

            migrationBuilder.DropTable(
                name: "CardTag");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.AlterColumn<string>(
                name: "TeamId",
                table: "Discussions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Companies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaskId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BucketId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Buckets_BucketId",
                        column: x => x.BucketId,
                        principalTable: "Buckets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagTask",
                columns: table => new
                {
                    TagsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TasksId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTask", x => new { x.TagsId, x.TasksId });
                    table.ForeignKey(
                        name: "FK_TagTask_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TagTask_Tasks_TasksId",
                        column: x => x.TasksId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ApplicationUserId",
                table: "Companies",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TaskId",
                table: "AspNetUsers",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TagTask_TasksId",
                table: "TagTask",
                column: "TasksId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BucketId",
                table: "Tasks",
                column: "BucketId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_IsDeleted",
                table: "Tasks",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Tasks_TaskId",
                table: "AspNetUsers",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_ApplicationUserId",
                table: "Companies",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
