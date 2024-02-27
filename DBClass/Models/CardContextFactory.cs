using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DBClass.Models
{
    public class CardContextFactory : IDesignTimeDbContextFactory<CardContext>
    {
        public CardContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CardContext>();
            optionsBuilder.UseMySQL("Server=localhost;Port=3333;User ID=root;Password=secret;Database=mma_calendar");

            return new CardContext(optionsBuilder.Options);
        }

        /*
         * Hacky way to get the card context when trying to create a controller using aspnet code generator 
         */
        public CardContext CreateDbContextController(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CardContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Port=3333;User ID=root;Password=secret;Database=mma_calendar");

            return new CardContext(optionsBuilder.Options);
        }
    }
}