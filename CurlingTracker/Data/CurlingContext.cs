using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CurlingTracker.Models;
using System.Linq;

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

        private string ConnectionString;

        public CurlingContext(DbContextOptions<CurlingContext> options) : base(options)
        {
        }

        public CurlingContext(string connectionString, DbContextOptions<CurlingContext> options) : base(options)
        {
            this.ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConnectionString = (ConnectionString != null ? ConnectionString : "DataSource=app.db");
            optionsBuilder.UseSqlite(ConnectionString);
        }
    }
}