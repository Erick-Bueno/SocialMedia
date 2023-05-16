﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CommentModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Posts_id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("User_id")
                        .HasColumnType("char(36)");

                    b.Property<string>("comment")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("varchar(600)");

                    b.HasKey("id");

                    b.HasIndex("Posts_id");

                    b.HasIndex("User_id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("FriendsModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("user_id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("user_id_2")
                        .HasColumnType("char(36)");

                    b.HasKey("id");

                    b.HasIndex("user_id");

                    b.HasIndex("user_id_2");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("LikesModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Posts_id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Users_id")
                        .HasColumnType("char(36)");

                    b.HasKey("id");

                    b.HasIndex("Posts_id");

                    b.HasIndex("Users_id");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("PostImagesModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("imgUrl")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("varchar(600)");

                    b.Property<Guid>("posts_id")
                        .HasColumnType("char(36)");

                    b.HasKey("id");

                    b.HasIndex("posts_id");

                    b.ToTable("Posts_images");
                });

            modelBuilder.Entity("PostModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ContentPost")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("varchar(600)");

                    b.Property<DateTime>("DatePost")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Tot_likes")
                        .HasColumnType("int");

                    b.Property<Guid>("User_id")
                        .HasColumnType("char(36)");

                    b.Property<int>("tot_comments")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("User_id");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("RequestsModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Receiver_id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("RequestDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("Requester_id")
                        .HasColumnType("char(36)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Receiver_id");

                    b.HasIndex("Requester_id");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("TokenModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("jwt")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.HasKey("id");

                    b.ToTable("Token");
                });

            modelBuilder.Entity("UserModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("varchar(600)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("User_Photo")
                        .HasMaxLength(600)
                        .HasColumnType("varchar(600)");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CommentModel", b =>
                {
                    b.HasOne("PostModel", "postModel")
                        .WithMany("commentsPost")
                        .HasForeignKey("Posts_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserModel", "userModel")
                        .WithMany("userComments")
                        .HasForeignKey("User_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("postModel");

                    b.Navigation("userModel");
                });

            modelBuilder.Entity("FriendsModel", b =>
                {
                    b.HasOne("UserModel", "userModel")
                        .WithMany("UsersFriends")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserModel", "userModel2")
                        .WithMany("UsersFriends2")
                        .HasForeignKey("user_id_2")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("userModel");

                    b.Navigation("userModel2");
                });

            modelBuilder.Entity("LikesModel", b =>
                {
                    b.HasOne("PostModel", "postModel")
                        .WithMany("postLikes")
                        .HasForeignKey("Posts_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserModel", "userModel")
                        .WithMany("userlikes")
                        .HasForeignKey("Users_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("postModel");

                    b.Navigation("userModel");
                });

            modelBuilder.Entity("PostImagesModel", b =>
                {
                    b.HasOne("PostModel", "posts")
                        .WithMany("postsimages")
                        .HasForeignKey("posts_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("posts");
                });

            modelBuilder.Entity("PostModel", b =>
                {
                    b.HasOne("UserModel", "user")
                        .WithMany("posts")
                        .HasForeignKey("User_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("RequestsModel", b =>
                {
                    b.HasOne("UserModel", "UserModel2")
                        .WithMany("usersRequests2")
                        .HasForeignKey("Receiver_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserModel", "userModel")
                        .WithMany("usersRequests")
                        .HasForeignKey("Requester_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserModel2");

                    b.Navigation("userModel");
                });

            modelBuilder.Entity("PostModel", b =>
                {
                    b.Navigation("commentsPost");

                    b.Navigation("postLikes");

                    b.Navigation("postsimages");
                });

            modelBuilder.Entity("UserModel", b =>
                {
                    b.Navigation("UsersFriends");

                    b.Navigation("UsersFriends2");

                    b.Navigation("posts");

                    b.Navigation("userComments");

                    b.Navigation("userlikes");

                    b.Navigation("usersRequests");

                    b.Navigation("usersRequests2");
                });
#pragma warning restore 612, 618
        }
    }
}
