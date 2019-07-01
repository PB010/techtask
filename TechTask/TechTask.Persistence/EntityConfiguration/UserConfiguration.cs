using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TechTask.Persistence.Models.Users;
using TechTask.Persistence.Models.Users.Enums;

namespace TechTask.Persistence.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Email).IsRequired();
            builder.Property(b => b.Password).IsRequired();
            builder.Property(b => b.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(b => b.LastName).IsRequired().HasMaxLength(50);
            builder.Property(b => b.DateOfBirth).IsRequired();
            builder.Property(b => b.Role).IsRequired();

            builder.HasData(
                new 
                {
                    Id = new Guid("bfd23b66-1a4e-41de-4aa7-08d6fae3b08b"),
                    DateOfBirth = new DateTime(1984, 09, 13),
                    FirstName = "Will",
                    LastName = "Stevens",
                    Email = "will.s@tech.com",
                    Password = "Will123",
                    Role = Roles.Admin,
                    TeamId = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new 
                {
                    Id = new Guid("76aebd31-0235-4ef3-a123-08d6fbc1bdcd"),
                    DateOfBirth = new DateTime(1993, 04, 28),
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "john.s@tech.com",
                    Password = "John123",
                    Role = Roles.User,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new 
                {
                    Id = new Guid("ed09fe47-84c0-47b5-8007-ae2ea4350a8b"),
                    DateOfBirth = new DateTime(1973, 10, 30),
                    FirstName = "Jane",
                    LastName = "Williams",
                    Email = "jane.w@tech.com",
                    Password = "Jane123",
                    Role = Roles.Admin,
                    TeamId = 2,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new 
                {
                    Id = new Guid("f3c88d42-fb42-43c4-a9d4-1a738a2bd20c"),
                    DateOfBirth = new DateTime(1988, 03, 31),
                    FirstName = "Anthony",
                    LastName = "Russell",
                    Email = "anthony.r@tech.com",
                    Password = "Anthony123",
                    Role = Roles.User,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });
        }
    }
}
