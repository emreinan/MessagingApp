﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Contexts;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240803174709_mig-2")]
    partial class mig2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedDate");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeletedDate");

                    b.Property<string>("ImageIdentifier")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ImageIdentifier");

                    b.Property<string>("InvitationCode")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("InvitationCode");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("Chats", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.ChatUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ChatId");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedDate");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeletedDate");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdatedDate");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("ChatUsers", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ChatId");

                    b.Property<string>("Content")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("Content");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedDate");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeletedDate");

                    b.Property<string>("FileIdentifier")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("FileIdentifier");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("SentAt");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdatedDate");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.MessageUserState", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedDate");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeletedDate");

                    b.Property<DateTime?>("DeliveredAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeliveredAt");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("MessageId");

                    b.Property<DateTime?>("ReadAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("ReadAt");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdatedDate");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.HasIndex("UserId");

                    b.ToTable("MessageUserStates", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("CreatedByIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CreatedByIp");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedDate");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeletedDate");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("ExpiresAt");

                    b.Property<string>("ReasonRevoked")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("ReasonRevoked");

                    b.Property<string>("ReplacedByToken")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("ReplacedByToken");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime2")
                        .HasColumnName("Revoked");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Token");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdatedDate");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedDate");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeletedDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Email");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit")
                        .HasColumnName("IsVerified");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Nickname");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("PasswordHash");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("PasswordSalt");

                    b.Property<string>("ProfileImageIdentifier")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("ProfileImageIdentifier");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdatedDate");

                    b.Property<string>("VerificationCode")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("VerificationCode");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("f324baf1-0204-4a24-a859-5279a6a82262"),
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@mail.com",
                            IsVerified = false,
                            Nickname = "Admin",
                            PasswordHash = new byte[] { 215, 131, 79, 242, 26, 130, 241, 37, 20, 150, 254, 238, 17, 112, 96, 245, 246, 2, 246, 226, 98, 11, 125, 76, 56, 0, 165, 217, 157, 33, 51, 49, 127, 109, 47, 64, 156, 50, 238, 41, 40, 204, 145, 52, 89, 14, 65, 248, 33, 16, 182, 120, 113, 177, 193, 125, 187, 233, 121, 27, 120, 156, 218, 165 },
                            PasswordSalt = new byte[] { 44, 171, 155, 80, 144, 81, 170, 194, 5, 92, 200, 190, 243, 185, 120, 115, 39, 221, 79, 92, 182, 164, 14, 119, 12, 250, 51, 252, 175, 138, 18, 74, 94, 193, 198, 66, 56, 111, 34, 203, 15, 118, 136, 57, 99, 36, 171, 16, 42, 51, 222, 180, 214, 248, 117, 76, 47, 120, 8, 188, 172, 89, 42, 74, 115, 66, 98, 188, 142, 43, 196, 191, 33, 163, 150, 204, 53, 225, 95, 62, 134, 154, 190, 204, 161, 113, 136, 148, 37, 184, 175, 31, 208, 213, 118, 206, 148, 106, 20, 161, 159, 78, 215, 252, 189, 82, 66, 192, 239, 139, 177, 66, 43, 26, 231, 142, 134, 125, 194, 64, 122, 190, 52, 187, 79, 29, 178, 67 }
                        });
                });

            modelBuilder.Entity("Domain.Entities.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId", "RoleId")
                        .IsUnique();

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.ChatUser", b =>
                {
                    b.HasOne("Domain.Entities.Chat", "Chat")
                        .WithMany("ChatUsers")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("ChatUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Message", b =>
                {
                    b.HasOne("Domain.Entities.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.MessageUserState", b =>
                {
                    b.HasOne("Domain.Entities.Message", "Message")
                        .WithMany("MessageUserStates")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("MessageUserStates")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Message");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.UserRole", b =>
                {
                    b.HasOne("Domain.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Chat", b =>
                {
                    b.Navigation("ChatUsers");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Domain.Entities.Message", b =>
                {
                    b.Navigation("MessageUserStates");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("ChatUsers");

                    b.Navigation("MessageUserStates");

                    b.Navigation("Messages");

                    b.Navigation("RefreshTokens");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
