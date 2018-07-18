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



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseMySql(@"server=localhost;port=3306;database=db;uid=root;password=jikipol");
        }
    }
}