using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class RefactoredColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_Posts_id",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_User_id",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Users_user_id",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Users_user_id_2",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Posts_Posts_id",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_Users_id",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_User_id",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_images_Posts_posts_id",
                table: "Posts_images");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_Receiver_id",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_Requester_id",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "userName");

            migrationBuilder.RenameColumn(
                name: "Telephone",
                table: "Users",
                newName: "telephone");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "User_Photo",
                table: "Users",
                newName: "userPhoto");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Token",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Requester_id",
                table: "Requests",
                newName: "requesterId");

            migrationBuilder.RenameColumn(
                name: "Receiver_id",
                table: "Requests",
                newName: "receiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_Requester_id",
                table: "Requests",
                newName: "IX_Requests_requesterId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_Receiver_id",
                table: "Requests",
                newName: "IX_Requests_receiverId");

            migrationBuilder.RenameColumn(
                name: "posts_id",
                table: "Posts_images",
                newName: "postId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_images_posts_id",
                table: "Posts_images",
                newName: "IX_Posts_images_postId");

            migrationBuilder.RenameColumn(
                name: "DatePost",
                table: "Posts",
                newName: "datePost");

            migrationBuilder.RenameColumn(
                name: "ContentPost",
                table: "Posts",
                newName: "contentPost");

            migrationBuilder.RenameColumn(
                name: "tot_comments",
                table: "Posts",
                newName: "totalLikes");

            migrationBuilder.RenameColumn(
                name: "User_id",
                table: "Posts",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Tot_likes",
                table: "Posts",
                newName: "totalComments");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_User_id",
                table: "Posts",
                newName: "IX_Posts_userId");

            migrationBuilder.RenameColumn(
                name: "Users_id",
                table: "Likes",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Posts_id",
                table: "Likes",
                newName: "postId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_Users_id",
                table: "Likes",
                newName: "IX_Likes_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_Posts_id",
                table: "Likes",
                newName: "IX_Likes_postId");

            migrationBuilder.RenameColumn(
                name: "user_id_2",
                table: "Friends",
                newName: "userId2");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Friends",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_user_id_2",
                table: "Friends",
                newName: "IX_Friends_userId2");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_user_id",
                table: "Friends",
                newName: "IX_Friends_userId");

            migrationBuilder.RenameColumn(
                name: "User_id",
                table: "Comments",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Posts_id",
                table: "Comments",
                newName: "postId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_User_id",
                table: "Comments",
                newName: "IX_Comments_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_Posts_id",
                table: "Comments",
                newName: "IX_Comments_postId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_postId",
                table: "Comments",
                column: "postId",
                principalTable: "Posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_userId",
                table: "Comments",
                column: "userId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Users_userId",
                table: "Friends",
                column: "userId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Users_userId2",
                table: "Friends",
                column: "userId2",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Posts_postId",
                table: "Likes",
                column: "postId",
                principalTable: "Posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_userId",
                table: "Likes",
                column: "userId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_userId",
                table: "Posts",
                column: "userId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_images_Posts_postId",
                table: "Posts_images",
                column: "postId",
                principalTable: "Posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_receiverId",
                table: "Requests",
                column: "receiverId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_requesterId",
                table: "Requests",
                column: "requesterId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_postId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_userId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Users_userId",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Users_userId2",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Posts_postId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_userId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_userId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_images_Posts_postId",
                table: "Posts_images");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_receiverId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_requesterId",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "userName",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "telephone",
                table: "Users",
                newName: "Telephone");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "userPhoto",
                table: "Users",
                newName: "User_Photo");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Token",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "requesterId",
                table: "Requests",
                newName: "Requester_id");

            migrationBuilder.RenameColumn(
                name: "receiverId",
                table: "Requests",
                newName: "Receiver_id");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_requesterId",
                table: "Requests",
                newName: "IX_Requests_Requester_id");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_receiverId",
                table: "Requests",
                newName: "IX_Requests_Receiver_id");

            migrationBuilder.RenameColumn(
                name: "postId",
                table: "Posts_images",
                newName: "posts_id");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_images_postId",
                table: "Posts_images",
                newName: "IX_Posts_images_posts_id");

            migrationBuilder.RenameColumn(
                name: "datePost",
                table: "Posts",
                newName: "DatePost");

            migrationBuilder.RenameColumn(
                name: "contentPost",
                table: "Posts",
                newName: "ContentPost");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Posts",
                newName: "User_id");

            migrationBuilder.RenameColumn(
                name: "totalLikes",
                table: "Posts",
                newName: "tot_comments");

            migrationBuilder.RenameColumn(
                name: "totalComments",
                table: "Posts",
                newName: "Tot_likes");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_userId",
                table: "Posts",
                newName: "IX_Posts_User_id");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Likes",
                newName: "Users_id");

            migrationBuilder.RenameColumn(
                name: "postId",
                table: "Likes",
                newName: "Posts_id");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_userId",
                table: "Likes",
                newName: "IX_Likes_Users_id");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_postId",
                table: "Likes",
                newName: "IX_Likes_Posts_id");

            migrationBuilder.RenameColumn(
                name: "userId2",
                table: "Friends",
                newName: "user_id_2");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Friends",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_userId2",
                table: "Friends",
                newName: "IX_Friends_user_id_2");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_userId",
                table: "Friends",
                newName: "IX_Friends_user_id");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Comments",
                newName: "User_id");

            migrationBuilder.RenameColumn(
                name: "postId",
                table: "Comments",
                newName: "Posts_id");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_userId",
                table: "Comments",
                newName: "IX_Comments_User_id");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_postId",
                table: "Comments",
                newName: "IX_Comments_Posts_id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Users_user_id",
                table: "Friends",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Users_user_id_2",
                table: "Friends",
                column: "user_id_2",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Posts_Users_User_id",
                table: "Posts",
                column: "User_id",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_Receiver_id",
                table: "Requests",
                column: "Receiver_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_Requester_id",
                table: "Requests",
                column: "Requester_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
