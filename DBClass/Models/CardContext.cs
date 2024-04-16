using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DBClass.Models
{
    public class CardContext : DbContext
    {
        public CardContext(DbContextOptions<CardContext> options) : base(options)
        {
        }
        
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Fighter> Fighters { get; set; }

        public DbSet<Event> Events{ get; set; } 

        public DbSet<Fight> Fights { get; set; }
        

        public static CardContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CardContext>();
            optionsBuilder.UseMySQL("Server=localhost;Port=3333;User ID=root;Password=secret;Database=mma_calendar");

            return new CardContext(optionsBuilder.Options);
        }

        /*
         * Hacky way to get the card context when trying to create a controller using aspnet code generator 
         */
        public static CardContext CreateDbContextController(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CardContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Port=3333;User ID=root;Password=secret;Database=mma_calendar");

            return new CardContext(optionsBuilder.Options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fighter>()
                .ToTable("Fighters");
            modelBuilder.Entity<Venue>()
                .ToTable("Venues");
            modelBuilder.Entity<Event>()
                .ToTable("Events");
            modelBuilder.Entity<Fight>()
                .ToTable("Fights");
        }
    }
}