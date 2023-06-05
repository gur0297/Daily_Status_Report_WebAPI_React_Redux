﻿// <auto-generated />
using System;
using Daily_Status_Report_task.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Daily_Status_Report_task.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230605063055_initUserTable")]
    partial class initUserTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Daily_Status_Report_task.Models.StatusTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Hours")
                        .HasColumnType("int");

                    b.Property<bool>("Is_Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("NextDayPlan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Obstacle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Success")
                        .HasColumnType("bit");

                    b.Property<string>("TaskName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserTableId")
                        .HasColumnType("int");

                    b.Property<int>("User_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserTableId");

                    b.ToTable("StatusTables");
                });

            modelBuilder.Entity("Daily_Status_Report_task.Models.UserTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Email_Register_Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Is_Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserTables");
                });

            modelBuilder.Entity("Daily_Status_Report_task.Models.StatusTable", b =>
                {
                    b.HasOne("Daily_Status_Report_task.Models.UserTable", "UserTable")
                        .WithMany()
                        .HasForeignKey("UserTableId");

                    b.Navigation("UserTable");
                });
#pragma warning restore 612, 618
        }
    }
}
