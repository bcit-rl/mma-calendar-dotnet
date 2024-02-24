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
        
        // public DbSet<Event> Events { get; set; }
        // public DbSet<Fight> Fights { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Fighter> Fighters { get; set; }

        public DbSet<Event> Events{ get; set; } 

        public DbSet<Fight> Fights { get; set; }
        
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