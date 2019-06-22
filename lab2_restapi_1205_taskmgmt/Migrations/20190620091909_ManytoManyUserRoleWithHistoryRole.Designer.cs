﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using lab2_restapi_1205_taskmgmt.Models;

namespace lab2_restapi_1205_taskmgmt.Migrations
{
    [DbContext(typeof(TasksDbContext))]
    [Migration("20190620091909_ManytoManyUserRoleWithHistoryRole")]
    partial class ManytoManyUserRoleWithHistoryRole
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("lab2_restapi_1205_taskmgmt.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Important");

                    b.Property<int?>("OwnerId");

                    b.Property<int?>("TaskId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("TaskId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("lab2_restapi_1205_taskmgmt.Models.HistoryUserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EndTime");

                    b.Property<DateTime?>("StartTime");

                    b.Property<int>("UserId");

                    b.Property<int>("UserRoleId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("UserRoleId");

                    b.ToTable("HistoryUserRoles");
                });

            modelBuilder.Entity("lab2_restapi_1205_taskmgmt.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Added");

                    b.Property<DateTime?>("ClosedAt");

                    b.Property<DateTime?>("Deadline");

                    b.Property<string>("Description");

                    b.Property<int>("Importance");

                    b.Property<int?>("OwnerId");

                    b.Property<int>("State");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("lab2_restapi_1205_taskmgmt.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateRegister");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("lab2_restapi_1205_taskmgmt.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("lab2_restapi_1205_taskmgmt.Models.Comment", b =>
                {
                    b.HasOne("lab2_restapi_1205_taskmgmt.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.HasOne("lab2_restapi_1205_taskmgmt.Models.Task")
                        .WithMany("Comments")
                        .HasForeignKey("TaskId");
                });

            modelBuilder.Entity("lab2_restapi_1205_taskmgmt.Models.HistoryUserRole", b =>
                {
                    b.HasOne("lab2_restapi_1205_taskmgmt.Models.User", "User")
                        .WithMany("HistoryUserRole")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("lab2_restapi_1205_taskmgmt.Models.UserRole", "UserRole")
                        .WithMany("HistoryUserRole")
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("lab2_restapi_1205_taskmgmt.Models.Task", b =>
                {
                    b.HasOne("lab2_restapi_1205_taskmgmt.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });
#pragma warning restore 612, 618
        }
    }
}
