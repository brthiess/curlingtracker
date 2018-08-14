using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CurlingTracker.Models;

namespace CurlingTracker.Data
{
    public class CurlingContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Draw> Draws { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Linescore> Linescore { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }

        public CurlingContext(DbContextOptions<CurlingContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=app.db");
        }
    }
}