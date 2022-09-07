﻿using Microsoft.EntityFrameworkCore;
using SampleCQRSApplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Round> Round { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Team>().HasMany<Match>(x => x.HomeMatches).WithOne(x => x.HomeTeam).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Team>().HasMany<Match>(x => x.AwayMatches).WithOne(x => x.AwayTeam).OnDelete(DeleteBehavior.NoAction);
        }
    }
}