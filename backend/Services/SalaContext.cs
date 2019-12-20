using backend.Models;
using backend.Models.MapObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data
{
    public class SalaContext : DbContext
    {
        private static SalaContext instance = null;
        public SalaContext(DbContextOptions<SalaContext> options) : base(options)
        {
        }

        private static object threadLock = new object();

        public static SalaContext getInstance()
        {
            lock (threadLock)
            {
                if (instance == null)
                {
                    Debug.WriteLine(Startup.ConnectionString);
                    instance = new SalaContext(GetOptions(Startup.ConnectionString));
                }
                return instance;
            }
        }

        private static DbContextOptions<SalaContext> GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder<SalaContext>(), connectionString).Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasKey(g => new { g.Id });
        }

        public DbSet<Player> Player { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<PlayerItem> PlayerItems { get; set; }
        public DbSet<Map> Map { get; set; }
        public DbSet<Tree> Tree { get; set; }
        public DbSet<Water> Water { get; set; }
        public DbSet<Rock> Rock { get; set; }
        public DbSet<PlayerCutItems> PlayerCutItem { get; set; }
    }
}
