﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Sanchime.Identity.Contexts;

#nullable disable

namespace Sanchime.Identity.Migrations
{
    [DbContext(typeof(IdentityContext))]
    partial class IdentityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence("IdentityEntitySequence");

            modelBuilder.Entity("MenuRole", b =>
                {
                    b.Property<long>("MenusId")
                        .HasColumnType("bigint");

                    b.Property<long>("RolesId")
                        .HasColumnType("bigint");

                    b.HasKey("MenusId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("MenuRole");
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.Property<long>("PermissionsId")
                        .HasColumnType("bigint");

                    b.Property<long>("RolesId")
                        .HasColumnType("bigint");

                    b.HasKey("PermissionsId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("PermissionRole");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<long>("RolesId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsersId")
                        .HasColumnType("bigint");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("Sanchime.Identity.Entities.IdentityEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("nextval('\"IdentityEntitySequence\"')");

                    NpgsqlPropertyBuilderExtensions.UseSequence(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("CreatedUser")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedUserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<DateTimeOffset>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("ModifiedUser")
                        .HasColumnType("bigint");

                    b.Property<string>("ModifiedUserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("IsEnabled");

                    b.HasIndex("CreatedDate", "CreatedUser", "CreatedUserName", "ModifiedDate", "ModifiedUser", "ModifiedUserName")
                        .IsDescending();

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Sanchime.Identity.Entities.Account", b =>
                {
                    b.HasBaseType("Sanchime.Identity.Entities.IdentityEntity");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasIndex("LoginName")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Sanchime.Identity.Entities.Menu", b =>
                {
                    b.HasBaseType("Sanchime.Identity.Entities.IdentityEntity");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Icon")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.Property<string>("Route")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasIndex("ParentId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("Sanchime.Identity.Entities.Permission", b =>
                {
                    b.HasBaseType("Sanchime.Identity.Entities.IdentityEntity");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.HasIndex("ParentId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Sanchime.Identity.Entities.Role", b =>
                {
                    b.HasBaseType("Sanchime.Identity.Entities.IdentityEntity");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.HasIndex("ParentId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Sanchime.Identity.Entities.User", b =>
                {
                    b.HasBaseType("Sanchime.Identity.Entities.IdentityEntity");

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<string>("Avatar")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<long?>("UserGroupId")
                        .HasColumnType("bigint");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("UserGroupId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Sanchime.Identity.Entities.UserGroup", b =>
                {
                    b.HasBaseType("Sanchime.Identity.Entities.IdentityEntity");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.HasIndex("ParentId");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("MenuRole", b =>
                {
                    b.HasOne("Sanchime.Identity.Entities.Menu", null)
                        .WithMany()
                        .HasForeignKey("MenusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sanchime.Identity.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.HasOne("Sanchime.Identity.Entities.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sanchime.Identity.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("Sanchime.Identity.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sanchime.Identity.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sanchime.Identity.Entities.Menu", b =>
                {
                    b.HasOne("Sanchime.Identity.Entities.Menu", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Sanchime.Identity.Entities.Permission", b =>
                {
                    b.HasOne("Sanchime.Identity.Entities.Permission", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Sanchime.Identity.Entities.Role", b =>
                {
                    b.HasOne("Sanchime.Identity.Entities.Role", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Sanchime.Identity.Entities.User", b =>
                {
                    b.HasOne("Sanchime.Identity.Entities.Account", "Account")
                        .WithOne("User")
                        .HasForeignKey("Sanchime.Identity.Entities.User", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sanchime.Identity.Entities.UserGroup", "UserGroup")
                        .WithMany("Users")
                        .HasForeignKey("UserGroupId");

                    b.Navigation("Account");

                    b.Navigation("UserGroup");
                });

            modelBuilder.Entity("Sanchime.Identity.Entities.UserGroup", b =>
                {
                    b.HasOne("Sanchime.Identity.Entities.UserGroup", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Sanchime.Identity.Entities.Account", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("Sanchime.Identity.Entities.Menu", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("Sanchime.Identity.Entities.Permission", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("Sanchime.Identity.Entities.Role", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("Sanchime.Identity.Entities.UserGroup", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
