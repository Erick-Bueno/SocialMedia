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

                    b.Property<string>("comment")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("varchar(600)");

                    b.Property<Guid>("postId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("userId")
                        .HasColumnType("char(36)");

                    b.HasKey("id");

                    b.HasIndex("postId");

                    b.HasIndex("userId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("FriendsModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("userId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("userId2")
                        .HasColumnType("char(36)");

                    b.HasKey("id");

                    b.HasIndex("userId");

                    b.HasIndex("userId2");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("LikesModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("postId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("userId")
                        .HasColumnType("char(36)");

                    b.HasKey("id");

                    b.HasIndex("postId");

                    b.HasIndex("userId");

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

                    b.Property<Guid>("postId")
                        .HasColumnType("char(36)");

                    b.HasKey("id");

                    b.HasIndex("postId");

                    b.ToTable("Posts_images");
                });

            modelBuilder.Entity("PostModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("contentPost")
                        .HasColumnType("text");

                    b.Property<DateTime>("datePost")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("totalComments")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int?>("totalLikes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<Guid>("userId")
                        .HasColumnType("char(36)");

                    b.HasKey("id");

                    b.HasIndex("userId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("RequestsModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("RequestDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("receiverId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("requesterId")
                        .HasColumnType("char(36)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("receiverId");

                    b.HasIndex("requesterId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("TokenModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("email")
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

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("varchar(600)");

                    b.Property<string>("telephone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("userPhoto")
                        .HasMaxLength(600)
                        .HasColumnType("varchar(600)");

                    b.HasKey("id");

                    b.HasIndex("email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CommentModel", b =>
                {
                    b.HasOne("PostModel", "postModel")
                        .WithMany("commentsPost")
                        .HasForeignKey("postId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserModel", "userModel")
                        .WithMany("userComments")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("postModel");

                    b.Navigation("userModel");
                });

            modelBuilder.Entity("FriendsModel", b =>
                {
                    b.HasOne("UserModel", "userModel")
                        .WithMany("UsersFriends")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserModel", "userModel2")
                        .WithMany("UsersFriends2")
                        .HasForeignKey("userId2")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("userModel");

                    b.Navigation("userModel2");
                });

            modelBuilder.Entity("LikesModel", b =>
                {
                    b.HasOne("PostModel", "postModel")
                        .WithMany("postLikes")
                        .HasForeignKey("postId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserModel", "userModel")
                        .WithMany("userlikes")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("postModel");

                    b.Navigation("userModel");
                });

            modelBuilder.Entity("PostImagesModel", b =>
                {
                    b.HasOne("PostModel", "posts")
                        .WithMany("postsimages")
                        .HasForeignKey("postId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("posts");
                });

            modelBuilder.Entity("PostModel", b =>
                {
                    b.HasOne("UserModel", "user")
                        .WithMany("posts")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("RequestsModel", b =>
                {
                    b.HasOne("UserModel", "UserModel2")
                        .WithMany("usersRequests2")
                        .HasForeignKey("receiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserModel", "userModel")
                        .WithMany("usersRequests")
                        .HasForeignKey("requesterId")
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
