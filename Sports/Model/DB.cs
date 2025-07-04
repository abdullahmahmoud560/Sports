﻿using Microsoft.EntityFrameworkCore;

namespace Sports.Model
{
    public class DB : DbContext
    {
        public DB(DbContextOptions<DB> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Academy>().HasIndex(u => u.AcademyEmail).IsUnique();
            modelBuilder.Entity<Academy>().ToTable("academies");
            modelBuilder.Entity<Role>().ToTable("roles");
            modelBuilder.Entity<Match>().ToTable("matches");
            modelBuilder.Entity<Player>().ToTable("players");
            modelBuilder.Entity<Camp>().ToTable("camps");
        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Academy> Academies { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Camp> camps { get; set; }
    }
}
