using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class postsimages2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikesModel_Posts_Posts_id",
                table: "LikesModel");

            migrationBuilder.DropForeignKey(
                name: "FK_LikesModel_Users_Users_id",
                table: "LikesModel");

            migrationBuilder.DropForeignKey(
                name: "FK_PostImagesModel_Posts_posts_id",
                table: "PostImagesModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostImagesModel",
                table: "PostImagesModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LikesModel",
                table: "LikesModel");

            migrationBuilder.RenameTable(
                name: "PostImagesModel",
                newName: "Posts_images");

            migrationBuilder.RenameTable(
                name: "LikesModel",
                newName: "Likes");

            migrationBuilder.RenameIndex(
                name: "IX_PostImagesModel_posts_id",
                table: "Posts_images",
                newName: "IX_Posts_images_posts_id");

            migrationBuilder.RenameIndex(
                name: "IX_LikesModel_Users_id",
                table: "Likes",
                newName: "IX_Likes_Users_id");

            migrationBuilder.RenameIndex(
                name: "IX_LikesModel_Posts_id",
                table: "Likes",
                newName: "IX_Likes_Posts_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts_images",
                table: "Posts_images",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Posts_Posts_id",
                table: "Likes",
                column: "Posts_id",
                principalTable: "Posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_Users_id",
                table: "Likes",
                column: "Users_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_images_Posts_posts_id",
                table: "Posts_images",
                column: "posts_id",
                principalTable: "Posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Posts_Posts_id",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_Users_id",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_images_Posts_posts_id",
                table: "Posts_images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts_images",
                table: "Posts_images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.RenameTable(
                name: "Posts_images",
                newName: "PostImagesModel");

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "LikesModel");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_images_posts_id",
                table: "PostImagesModel",
                newName: "IX_PostImagesModel_posts_id");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_Users_id",
                table: "LikesModel",
                newName: "IX_LikesModel_Users_id");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_Posts_id",
                table: "LikesModel",
                newName: "IX_LikesModel_Posts_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostImagesModel",
                table: "PostImagesModel",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LikesModel",
                table: "LikesModel",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_LikesModel_Posts_Posts_id",
                table: "LikesModel",
                column: "Posts_id",
                principalTable: "Posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikesModel_Users_Users_id",
                table: "LikesModel",
                column: "Users_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostImagesModel_Posts_posts_id",
                table: "PostImagesModel",
                column: "posts_id",
                principalTable: "Posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
