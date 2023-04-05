using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class firends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Friends_user_id",
                table: "Friends",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Users_user_id",
                table: "Friends",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Users_user_id",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_Friends_user_id",
                table: "Friends");
        }
    }
}
