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
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;");
        }
    }
}