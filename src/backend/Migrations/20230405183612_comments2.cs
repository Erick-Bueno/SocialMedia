using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class comments2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_postModelid",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_userModelid",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_postModelid",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_userModelid",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "postModelid",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "userModelid",
                table: "Comments");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Posts_id",
                table: "Comments",
                column: "Posts_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_User_id",
                table: "Comments",
                column: "User_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_Posts_id",
                table: "Comments",
                column: "Posts_id",
                principalTable: "Posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_User_id",
                table: "Comments",
                column: "User_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_Posts_id",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_User_id",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_Posts_id",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_User_id",
                table: "Comments");

            migrationBuilder.AddColumn<Guid>(
                name: "postModelid",
                table: "Comments",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "userModelid",
                table: "Comments",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_postModelid",
                table: "Comments",
                column: "postModelid");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_userModelid",
                table: "Comments",
                column: "userModelid");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_postModelid",
                table: "Comments",
                column: "postModelid",
                principalTable: "Posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_userModelid",
                table: "Comments",
                column: "userModelid",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
