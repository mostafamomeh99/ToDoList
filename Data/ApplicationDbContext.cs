using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ToDoList.Models;
using System.Numerics;
namespace ToDoList.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            var connection = builder.GetConnectionString("DefaultConnection");


            optionsBuilder.UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasData(
                   new User { Id = 1, Name = "Mohamed"}
               );

           modelBuilder.Entity<TodoItem>().HasData(
                new TodoItem { Id = 1, Title="i want to read this pdf" ,Description="i want to know information",
                FileDescription="1.pdf",
                    DeadLine= DateTime.Now.AddDays(7),
                    UserId=1
                },
                  new TodoItem
                  {
                      Id = 2,
                      Title = "i want to do what in pdf",
                      Description = "i want to know information",
                      FileDescription = "2.pdf",
                      DeadLine = DateTime.Now.AddDays(8),
                      UserId = 1
                  }
            );
            modelBuilder.HasSequence<int>("Idtodos");
            modelBuilder.HasSequence<int>("Idusers");

            modelBuilder.Entity<TodoItem>()
                .Property(o => o.Id)
                .HasDefaultValueSql("NEXT VALUE FOR Idtodos");

            modelBuilder.Entity<User>()
              .Property(o => o.Id)
              .HasDefaultValueSql("NEXT VALUE FOR Idusers");

        }

    }
}
