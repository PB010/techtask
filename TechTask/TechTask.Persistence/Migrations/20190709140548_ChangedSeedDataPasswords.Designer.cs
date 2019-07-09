﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechTask.Persistence.Context;

namespace TechTask.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190709140548_ChangedSeedDataPasswords")]
    partial class ChangedSeedDataPasswords
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TechTask.Persistence.Models.Logs.UpdateLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("UpdateLogs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2019, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Added new User",
                            Value = "bfd23b66-1a4e-41de-4aa7-08d6fae3b08b"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2019, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Added new User",
                            Value = "76aebd31-0235-4ef3-a123-08d6fbc1bdcd"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2019, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Added new User",
                            Value = "ed09fe47-84c0-47b5-8007-ae2ea4350a8b"
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2019, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Added new User",
                            Value = "f3c88d42-fb42-43c4-a9d4-1a738a2bd20c"
                        },
                        new
                        {
                            Id = 5,
                            CreatedAt = new DateTime(2019, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Added new team",
                            Value = "1"
                        },
                        new
                        {
                            Id = 6,
                            CreatedAt = new DateTime(2019, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Added new team",
                            Value = "2"
                        },
                        new
                        {
                            Id = 7,
                            CreatedAt = new DateTime(2019, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Added new task",
                            Value = "1"
                        },
                        new
                        {
                            Id = 8,
                            CreatedAt = new DateTime(2019, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Added new log to task",
                            Value = "1"
                        },
                        new
                        {
                            Id = 9,
                            CreatedAt = new DateTime(2019, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Added new comment",
                            Value = "1"
                        });
                });

            modelBuilder.Entity("TechTask.Persistence.Models.Task.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<int>("TasksId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TasksId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "I'm pleased with your work for now.",
                            TasksId = 1,
                            UserId = new Guid("bfd23b66-1a4e-41de-4aa7-08d6fae3b08b")
                        });
                });

            modelBuilder.Entity("TechTask.Persistence.Models.Task.TaskPriority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("TaskPriorities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Low"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Normal"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Urgent"
                        });
                });

            modelBuilder.Entity("TechTask.Persistence.Models.Task.Tasks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdminApprovalOfTaskCompletion");

                    b.Property<int>("Balance");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<int>("EstimatedTimeToFinishInHours");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Status");

                    b.Property<int>("TaskPriorityId");

                    b.Property<int>("TeamId");

                    b.Property<int>("TotalHoursOfWork");

                    b.Property<string>("TrackerFirstName");

                    b.Property<Guid?>("TrackerId");

                    b.Property<string>("TrackerLastName");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TaskPriorityId");

                    b.HasIndex("TeamId");

                    b.HasIndex("UserId");

                    b.ToTable("Tasks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AdminApprovalOfTaskCompletion = 0,
                            Balance = 3,
                            Description = "Weekly maintenance",
                            EstimatedTimeToFinishInHours = 4,
                            Name = "Maintenance",
                            Status = 1,
                            TaskPriorityId = 2,
                            TeamId = 1,
                            TotalHoursOfWork = 1,
                            TrackerFirstName = "Will",
                            TrackerId = new Guid("bfd23b66-1a4e-41de-4aa7-08d6fae3b08b"),
                            TrackerLastName = "Stevens",
                            UserId = new Guid("76aebd31-0235-4ef3-a123-08d6fbc1bdcd")
                        });
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

                    b.Property<int>("HoursSpent");

                    b.Property<int>("TasksId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TasksId");

                    b.HasIndex("UserId");

                    b.ToTable("LoggedActivities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2019, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Started working on it.",
                            HoursSpent = 1,
                            TasksId = 1,
                            UserId = new Guid("76aebd31-0235-4ef3-a123-08d6fbc1bdcd")
                        });
                });

            modelBuilder.Entity("TechTask.Persistence.Models.Users.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HoursOfWorkOnAllTasks");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Teams");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            HoursOfWorkOnAllTasks = 1,
                            Name = "Alpha"
                        },
                        new
                        {
                            Id = 2,
                            HoursOfWorkOnAllTasks = 0,
                            Name = "Beta"
                        });
                });

            modelBuilder.Entity("TechTask.Persistence.Models.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

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

                    b.Property<int?>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("bfd23b66-1a4e-41de-4aa7-08d6fae3b08b"),
                            DateOfBirth = new DateTime(1984, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "will.s@tech.com",
                            FirstName = "Will",
                            LastName = "Stevens",
                            Password = "XwnePnyMST/z0kzsHhlzBpX+Wo3H+HMDHI221qWUJKe1Towf",
                            Role = 0,
                            TeamId = 1
                        },
                        new
                        {
                            Id = new Guid("76aebd31-0235-4ef3-a123-08d6fbc1bdcd"),
                            DateOfBirth = new DateTime(1993, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "john.s@tech.com",
                            FirstName = "John",
                            LastName = "Smith",
                            Password = "7QvxqokZvh+C79cWRlso+HdjtqHc3OZfRpHNrElEFhLyv8iP",
                            Role = 1
                        },
                        new
                        {
                            Id = new Guid("ed09fe47-84c0-47b5-8007-ae2ea4350a8b"),
                            DateOfBirth = new DateTime(1973, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "jane.w@tech.com",
                            FirstName = "Jane",
                            LastName = "Williams",
                            Password = "6wUwTkF0Po8dpEYJdPjV6aq2NyR1NW7Dxi0E7zHvVJncUUhF",
                            Role = 0,
                            TeamId = 2
                        },
                        new
                        {
                            Id = new Guid("f3c88d42-fb42-43c4-a9d4-1a738a2bd20c"),
                            DateOfBirth = new DateTime(1988, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "anthony.r@tech.com",
                            FirstName = "Anthony",
                            LastName = "Russell",
                            Password = "gUFuNWS8RcSqnTauVupMn2YDyUB5yjapA7pJjAn+fdwGOWXY",
                            Role = 1
                        });
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
                    b.HasOne("TechTask.Persistence.Models.Task.TaskPriority", "TaskPriority")
                        .WithMany()
                        .HasForeignKey("TaskPriorityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TechTask.Persistence.Models.Users.Team", "Team")
                        .WithMany("Tasks")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TechTask.Persistence.Models.Users.User", "User")
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
                    b.HasOne("TechTask.Persistence.Models.Users.Team", "Team")
                        .WithMany("Users")
                        .HasForeignKey("TeamId");
                });
#pragma warning restore 612, 618
        }
    }
}
