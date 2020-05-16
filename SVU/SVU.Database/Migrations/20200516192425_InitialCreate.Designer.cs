﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SVU.Database.DatabaseContext;

namespace SVU.Database.Migrations
{
    [DbContext(typeof(SVUDbContext))]
    [Migration("20200516192425_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SVU.Database.Models.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<string>("Name");

                    b.Property<string>("Note");

                    b.Property<Guid>("ProgramId");

                    b.Property<string>("ShortName");

                    b.HasKey("Id");

                    b.HasIndex("ProgramId");

                    b.ToTable("Courses","SVU");
                });

            modelBuilder.Entity("SVU.Database.Models.ExternalLink", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentType");

                    b.Property<Guid?>("CourseId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description");

                    b.Property<Guid?>("HomeworkId");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<string>("Note");

                    b.Property<Guid?>("ProgramId");

                    b.Property<Guid?>("SessionId");

                    b.Property<string>("Title");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("HomeworkId");

                    b.HasIndex("ProgramId");

                    b.HasIndex("SessionId");

                    b.ToTable("ExternalLinks","Application");
                });

            modelBuilder.Entity("SVU.Database.Models.HeartDisease", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Age");

                    b.Property<bool>("BloodSugar");

                    b.Property<string>("ChestPainType");

                    b.Property<DateTime>("CreationDate");

                    b.Property<bool>("Disease");

                    b.Property<bool>("ExerciceAngina");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<double>("MaxHeartRate");

                    b.Property<string>("Note");

                    b.Property<double>("RestBloodPressure");

                    b.Property<string>("RestElectro");

                    b.HasKey("Id");

                    b.ToTable("HeartDisease","SVUDataSet");
                });

            modelBuilder.Entity("SVU.Database.Models.Homework", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CourseId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<string>("Note");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Homeworks","Student");
                });

            modelBuilder.Entity("SVU.Database.Models.Program", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<string>("Name");

                    b.Property<string>("Note");

                    b.HasKey("Id");

                    b.ToTable("Programs","SVU");
                });

            modelBuilder.Entity("SVU.Database.Models.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CourseId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<string>("Note");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Sessions","SVU");
                });

            modelBuilder.Entity("SVU.Database.Models.Tennis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Humidity");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<string>("Note");

                    b.Property<string>("Outlook");

                    b.Property<string>("Play");

                    b.Property<string>("Temp");

                    b.Property<string>("Wind");

                    b.HasKey("Id");

                    b.ToTable("Tennis","SVUDataSet");
                });

            modelBuilder.Entity("SVU.Database.Models.Course", b =>
                {
                    b.HasOne("SVU.Database.Models.Program", "Program")
                        .WithMany("Courses")
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SVU.Database.Models.ExternalLink", b =>
                {
                    b.HasOne("SVU.Database.Models.Course")
                        .WithMany("Links")
                        .HasForeignKey("CourseId");

                    b.HasOne("SVU.Database.Models.Homework")
                        .WithMany("Links")
                        .HasForeignKey("HomeworkId");

                    b.HasOne("SVU.Database.Models.Program")
                        .WithMany("Links")
                        .HasForeignKey("ProgramId");

                    b.HasOne("SVU.Database.Models.Session")
                        .WithMany("Links")
                        .HasForeignKey("SessionId");
                });

            modelBuilder.Entity("SVU.Database.Models.Homework", b =>
                {
                    b.HasOne("SVU.Database.Models.Course", "Course")
                        .WithMany("Homeworks")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SVU.Database.Models.Session", b =>
                {
                    b.HasOne("SVU.Database.Models.Course", "Course")
                        .WithMany("Sessions")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
