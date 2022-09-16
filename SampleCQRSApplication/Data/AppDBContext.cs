using Microsoft.EntityFrameworkCore;
using SampleCQRSApplication.Authentication;
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
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<TournamentRound> TournamentRounds { get; set; }
        public DbSet<TournamentSeason> TournamentSeasons { get; set; }
        public DbSet<TournamentTeam> TournamentTeams { get; set; }

        //Authentication
        public DbSet<User> Users { get; set; }
        public DbSet<SendMail> SendMails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Team>().HasMany<Match>(x => x.HomeMatches).WithOne(x => x.HomeTeam).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Team>().HasMany<Match>(x => x.VisitingMatches).WithOne(x => x.VisitingTeam).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
