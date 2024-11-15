﻿// <auto-generated />
using System;
using EnglishTutorAI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EnglishTutorAI.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.Achievement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IconFileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Achievements");

                    b.HasData(
                        new
                        {
                            Id = new Guid("684f3efb-bf12-42aa-8ff4-17705f81d447"),
                            Description = "achievements.noviceTranslator.description",
                            IconFileName = "novice_translator_icon.png",
                            IsCompleted = false,
                            Name = "achievements.noviceTranslator.name"
                        },
                        new
                        {
                            Id = new Guid("b4631aaf-f4e1-419f-a073-8da3b86fb6b5"),
                            Description = "achievements.dedicatedTranslator.description",
                            IconFileName = "dedicated_translator_icon.png",
                            IsCompleted = false,
                            Name = "achievements.dedicatedTranslator.name"
                        },
                        new
                        {
                            Id = new Guid("34ff3dda-dcfa-4f35-bb1b-0b13344cbf70"),
                            Description = "achievements.flawlessTranslator.description",
                            IconFileName = "flawless_translator_icon.png",
                            IsCompleted = false,
                            Name = "achievements.flawlessTranslator.name"
                        });
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.AchievementLevel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AchievementId")
                        .HasColumnType("uuid");

                    b.Property<int>("Goal")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AchievementId");

                    b.ToTable("AchievementLevels");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ee5e587d-1050-445d-bee1-c0c74419c273"),
                            AchievementId = new Guid("684f3efb-bf12-42aa-8ff4-17705f81d447"),
                            Goal = 5
                        },
                        new
                        {
                            Id = new Guid("4300546d-7eb5-461b-b800-def078028ae4"),
                            AchievementId = new Guid("684f3efb-bf12-42aa-8ff4-17705f81d447"),
                            Goal = 10
                        },
                        new
                        {
                            Id = new Guid("a4c81451-3aec-43a4-990c-a70a0d1e2522"),
                            AchievementId = new Guid("684f3efb-bf12-42aa-8ff4-17705f81d447"),
                            Goal = 30
                        },
                        new
                        {
                            Id = new Guid("87621b97-bd01-4db1-b308-0547c9a09559"),
                            AchievementId = new Guid("b4631aaf-f4e1-419f-a073-8da3b86fb6b5"),
                            Goal = 7
                        },
                        new
                        {
                            Id = new Guid("c32f0b45-1c31-420c-ac47-a9c2692838c0"),
                            AchievementId = new Guid("b4631aaf-f4e1-419f-a073-8da3b86fb6b5"),
                            Goal = 30
                        },
                        new
                        {
                            Id = new Guid("9ebfd7f8-5309-4838-8dfd-64dc7f5741ca"),
                            AchievementId = new Guid("b4631aaf-f4e1-419f-a073-8da3b86fb6b5"),
                            Goal = 100
                        },
                        new
                        {
                            Id = new Guid("214d3c6f-2e12-47cb-923f-92b33f27f2c8"),
                            AchievementId = new Guid("34ff3dda-dcfa-4f35-bb1b-0b13344cbf70"),
                            Goal = 10
                        },
                        new
                        {
                            Id = new Guid("aeb666eb-5192-4dd7-9d55-2e00dbaea370"),
                            AchievementId = new Guid("34ff3dda-dcfa-4f35-bb1b-0b13344cbf70"),
                            Goal = 30
                        },
                        new
                        {
                            Id = new Guid("72a62120-25e8-4a23-b639-694c73a1010c"),
                            AchievementId = new Guid("34ff3dda-dcfa-4f35-bb1b-0b13344cbf70"),
                            Goal = 100
                        });
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.DialogMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ConversationRole")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ThreadId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DialogMessages");
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CurrentLine")
                        .HasColumnType("integer");

                    b.Property<int>("StudyTopic")
                        .HasColumnType("integer");

                    b.Property<string>("ThreadId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.DocumentSentence", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DocumentId")
                        .HasColumnType("uuid");

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.ToTable("DocumentSentence");
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.DocumentSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DocumentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.ToTable("DocumentSessions");
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.LinguaFixMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CorrectedText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DocumentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DocumentSessionId")
                        .HasColumnType("uuid");

                    b.Property<string>("ThreadId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TranslatedText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("DocumentSessionId");

                    b.ToTable("LinguaFixMessages");
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("17b372ae-9678-49f6-bbe6-a90ae8e3c6ab"),
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = new Guid("9e2c2f8b-410e-422f-a472-dae5bf8f7ee7"),
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.UserAchievement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AchievementId")
                        .HasColumnType("uuid");

                    b.Property<int>("CurrentLevel")
                        .HasColumnType("integer");

                    b.Property<int>("Progress")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AchievementId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAchievements");
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.UserDocumentCompletion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DocumentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserDocumentCompletions");
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.UserSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsValid")
                        .HasColumnType("boolean");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserSessions");
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.UserStatistics", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CorrectedMistakes")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserStatistics");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.AchievementLevel", b =>
                {
                    b.HasOne("EnglishTutorAI.Domain.Entities.Achievement", "Achievement")
                        .WithMany("AchievementLevels")
                        .HasForeignKey("AchievementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Achievement");
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.DocumentSentence", b =>
                {
                    b.HasOne("EnglishTutorAI.Domain.Entities.Document", null)
                        .WithMany("Sentences")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.DocumentSession", b =>
                {
                    b.HasOne("EnglishTutorAI.Domain.Entities.Document", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.LinguaFixMessage", b =>
                {
                    b.HasOne("EnglishTutorAI.Domain.Entities.Document", null)
                        .WithMany("LinguaFixMessages")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnglishTutorAI.Domain.Entities.DocumentSession", "DocumentSession")
                        .WithMany()
                        .HasForeignKey("DocumentSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentSession");
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.UserAchievement", b =>
                {
                    b.HasOne("EnglishTutorAI.Domain.Entities.Achievement", "Achievement")
                        .WithMany()
                        .HasForeignKey("AchievementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnglishTutorAI.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Achievement");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.UserDocumentCompletion", b =>
                {
                    b.HasOne("EnglishTutorAI.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.UserSession", b =>
                {
                    b.HasOne("EnglishTutorAI.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.UserStatistics", b =>
                {
                    b.HasOne("EnglishTutorAI.Domain.Entities.User", "User")
                        .WithOne("UserStatistics")
                        .HasForeignKey("EnglishTutorAI.Domain.Entities.UserStatistics", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("EnglishTutorAI.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("EnglishTutorAI.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("EnglishTutorAI.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("EnglishTutorAI.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnglishTutorAI.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("EnglishTutorAI.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.Achievement", b =>
                {
                    b.Navigation("AchievementLevels");
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.Document", b =>
                {
                    b.Navigation("LinguaFixMessages");

                    b.Navigation("Sentences");
                });

            modelBuilder.Entity("EnglishTutorAI.Domain.Entities.User", b =>
                {
                    b.Navigation("UserStatistics")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
