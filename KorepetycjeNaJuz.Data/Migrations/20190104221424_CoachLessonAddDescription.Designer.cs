﻿// <auto-generated />
using System;
using KorepetycjeNaJuz.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KorepetycjeNaJuz.Data.Migrations
{
    [DbContext(typeof(KorepetycjeContext))]
    [Migration("20190104221424_CoachLessonAddDescription")]
    partial class CoachLessonAddDescription
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KorepetycjeNaJuz.Core.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasMaxLength(100);

                    b.Property<int>("CoachId");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Street")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasIndex("CoachId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("KorepetycjeNaJuz.Core.Models.CoachLesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId");

                    b.Property<int>("CoachId");

                    b.Property<DateTime>("DateEnd");

                    b.Property<DateTime>("DateStart");

                    b.Property<string>("Description");

                    b.Property<int>("LessonStatusId");

                    b.Property<int>("LessonSubjectId");

                    b.Property<decimal>("RatePerHour");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CoachId");

                    b.HasIndex("LessonStatusId");

                    b.HasIndex("LessonSubjectId");

                    b.ToTable("CoachLessons");
                });

            modelBuilder.Entity("KorepetycjeNaJuz.Core.Models.CoachLessonLevel", b =>
                {
                    b.Property<int>("CoachLessonId");

                    b.Property<int>("LessonLevelId");

                    b.HasKey("CoachLessonId", "LessonLevelId");

                    b.HasIndex("LessonLevelId");

                    b.ToTable("CoachLessonLevels");
                });

            modelBuilder.Entity("KorepetycjeNaJuz.Core.Models.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CoachLessonId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("LessonStatusId");

                    b.Property<float>("NumberOfHours");

                    b.Property<string>("OpinionOfCoach")
                        .HasMaxLength(2000);

                    b.Property<string>("OpinionOfStudent")
                        .HasMaxLength(2000);

                    b.Property<byte?>("RatingOfCoach");

                    b.Property<byte?>("RatingOfStudent");

                    b.Property<int>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("CoachLessonId");

                    b.HasIndex("LessonStatusId");

                    b.HasIndex("StudentId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("KorepetycjeNaJuz.Core.Models.LessonLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LevelName")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("LessonLevels");
                });

            modelBuilder.Entity("KorepetycjeNaJuz.Core.Models.LessonStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("LessonStatuses");
                });

            modelBuilder.Entity("KorepetycjeNaJuz.Core.Models.LessonSubject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("LessonSubjects");
                });

            modelBuilder.Entity("KorepetycjeNaJuz.Core.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("DateOfSending");

                    b.Property<int>("OwnerId");

                    b.Property<int>("RecipientId");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("RecipientId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("KorepetycjeNaJuz.Core.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Avatar");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<bool>("CookiesConfirmed");

                    b.Property<string>("Description")
                        .HasMaxLength(2000);

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .HasMaxLength(255);

                    b.Property<string>("LastName")
                        .HasMaxLength(255);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<bool>("PrivacyPolicesConfirmed");

                    b.Property<bool>("RodoConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Telephone")
                        .HasMaxLength(15);

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("KorepetycjeNaJuz.Core.Models.Address", b =>
                {
                    b.HasOne("KorepetycjeNaJuz.Core.Models.User", "Coach")
                        .WithMany()
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KorepetycjeNaJuz.Core.Models.CoachLesson", b =>
                {
                    b.HasOne("KorepetycjeNaJuz.Core.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KorepetycjeNaJuz.Core.Models.User", "Coach")
                        .WithMany()
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KorepetycjeNaJuz.Core.Models.LessonStatus", "LessonStatus")
                        .WithMany()
                        .HasForeignKey("LessonStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KorepetycjeNaJuz.Core.Models.LessonSubject", "Subject")
                        .WithMany()
                        .HasForeignKey("LessonSubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KorepetycjeNaJuz.Core.Models.CoachLessonLevel", b =>
                {
                    b.HasOne("KorepetycjeNaJuz.Core.Models.CoachLesson", "CoachLesson")
                        .WithMany("LessonLevels")
                        .HasForeignKey("CoachLessonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KorepetycjeNaJuz.Core.Models.LessonLevel", "LessonLevel")
                        .WithMany("CoachLessons")
                        .HasForeignKey("LessonLevelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KorepetycjeNaJuz.Core.Models.Lesson", b =>
                {
                    b.HasOne("KorepetycjeNaJuz.Core.Models.CoachLesson", "CoachLesson")
                        .WithMany("Lessons")
                        .HasForeignKey("CoachLessonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KorepetycjeNaJuz.Core.Models.LessonStatus", "LessonStatus")
                        .WithMany()
                        .HasForeignKey("LessonStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KorepetycjeNaJuz.Core.Models.User", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KorepetycjeNaJuz.Core.Models.Message", b =>
                {
                    b.HasOne("KorepetycjeNaJuz.Core.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KorepetycjeNaJuz.Core.Models.User", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("KorepetycjeNaJuz.Core.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("KorepetycjeNaJuz.Core.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KorepetycjeNaJuz.Core.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("KorepetycjeNaJuz.Core.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
