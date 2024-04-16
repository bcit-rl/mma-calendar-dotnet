using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DBClass.Models;

namespace CardContextFactory
{
    public class CreateDbContextController : DbContext
    {
        public CreateDbContextController (DbContextOptions<CreateDbContextController> options)
            : base(options)
        {
        }

        public DbSet<DBClass.Models.Venue> Venue { get; set; } = default!;
        public DbSet<DBClass.Models.Event> Event { get; set; } = default!;
        public DbSet<DBClass.Models.Fight> Fight { get; set; } = default!;
        public DbSet<DBClass.Models.Fighter> Fighter { get; set; } = default!;
    }
}
