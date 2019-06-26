﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechTask.Persistence.Context;

namespace TechTask.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TechTask.Persistence.Models.Task.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<int>("TasksId");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TasksId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("TechTask.Persistence.Models.Task.TaskPriority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("TaskPriorities");
                });

            modelBuilder.Entity("TechTask.Persistence.Models.Task.Tasks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Balance");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<DateTime>("EstimatedTimeToFinish");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("PriorityId");

                    b.Property<int>("Status");

                    b.Property<int?>("TeamId");

                    b.Property<DateTime>("TotalHoursOfWork");

                    b.Property<Guid?>("TrackerId");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PriorityId");

                    b.HasIndex("TeamId");

                    b.HasIndex("UserId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TechTask.Persistence.Models.Users.LoggedActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<DateTime>("HoursSpent");

                    b.Property<int>("TasksId");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TasksId");

                    b.HasIndex("UserId");

                    b.ToTable("LoggedActivities");
                });

            modelBuilder.Entity("TechTask.Persistence.Models.Users.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("HoursOfWorkOnAllTasks");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("TechTask.Persistence.Models.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<int>("Role");

                    b.Property<int>("TeamId");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TechTask.Persistence.Models.Task.Comment", b =>
                {
                    b.HasOne("TechTask.Persistence.Models.Task.Tasks")
                        .WithMany("Comments")
                        .HasForeignKey("TasksId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TechTask.Persistence.Models.Users.User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TechTask.Persistence.Models.Task.Tasks", b =>
                {
                    b.HasOne("TechTask.Persistence.Models.Task.TaskPriority", "Priority")
                        .WithMany()
                        .HasForeignKey("PriorityId");

                    b.HasOne("TechTask.Persistence.Models.Users.Team")
                        .WithMany("Tasks")
                        .HasForeignKey("TeamId");

                    b.HasOne("TechTask.Persistence.Models.Users.User")
                        .WithMany("Tasks")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TechTask.Persistence.Models.Users.LoggedActivity", b =>
                {
                    b.HasOne("TechTask.Persistence.Models.Task.Tasks")
                        .WithMany("Log")
                        .HasForeignKey("TasksId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TechTask.Persistence.Models.Users.User")
                        .WithMany("Log")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TechTask.Persistence.Models.Users.User", b =>
                {
                    b.HasOne("TechTask.Persistence.Models.Users.Team")
                        .WithMany("Users")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
