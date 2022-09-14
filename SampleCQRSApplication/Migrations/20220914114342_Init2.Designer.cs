﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleCQRSApplication.Data;

#nullable disable

namespace SampleCQRSApplication.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20220914114342_Init2")]
    partial class Init2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SampleCQRSApplication.Authentication.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.Bet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Decision")
                        .HasColumnType("int");

                    b.Property<int>("MatchId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.HasIndex("UserId");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("HomeTeamId")
                        .HasColumnType("int");

                    b.Property<DateTime>("MatchDate")
                        .HasColumnType("datetime(6)");

                    b.Property<float>("RateAwayTeam")
                        .HasColumnType("float");

                    b.Property<float>("RateHomeTeam")
                        .HasColumnType("float");

                    b.Property<int>("RoundId")
                        .HasColumnType("int");

                    b.Property<int>("ScoreAwayTeam")
                        .HasColumnType("int");

                    b.Property<int>("ScoreHomeTeam")
                        .HasColumnType("int");

                    b.Property<int>("VisitingTeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HomeTeamId");

                    b.HasIndex("RoundId");

                    b.HasIndex("VisitingTeamId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.Round", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("RoundId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoundId");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("SeasonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.SendMail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Sent")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Used")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("ValidateCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("SendMails");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LogoUrl")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("LogoUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("TournamentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.TournamentRound", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("RoundId")
                        .HasColumnType("int");

                    b.Property<int>("TournamentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TournamentRounds");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.TournamentSeason", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("SeasonId")
                        .HasColumnType("int");

                    b.Property<int>("TournamentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TournamentSeasons");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.TournamentTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<int>("TournamentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TournamentTeams");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.Bet", b =>
                {
                    b.HasOne("SampleCQRSApplication.DTO.Match", "Match")
                        .WithMany()
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SampleCQRSApplication.Authentication.User", "User")
                        .WithMany("Bets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Match");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.Match", b =>
                {
                    b.HasOne("SampleCQRSApplication.DTO.Team", "HomeTeam")
                        .WithMany("HomeMatches")
                        .HasForeignKey("HomeTeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SampleCQRSApplication.DTO.Round", "Round")
                        .WithMany("Matches")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SampleCQRSApplication.DTO.Team", "VisitingTeam")
                        .WithMany("VisitingMatches")
                        .HasForeignKey("VisitingTeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("HomeTeam");

                    b.Navigation("Round");

                    b.Navigation("VisitingTeam");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.Round", b =>
                {
                    b.HasOne("SampleCQRSApplication.DTO.TournamentRound", null)
                        .WithMany("Rounds")
                        .HasForeignKey("RoundId");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.Season", b =>
                {
                    b.HasOne("SampleCQRSApplication.DTO.TournamentSeason", null)
                        .WithMany("Seasons")
                        .HasForeignKey("SeasonId");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.SendMail", b =>
                {
                    b.HasOne("SampleCQRSApplication.Authentication.User", "User")
                        .WithMany("SendMails")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.Team", b =>
                {
                    b.HasOne("SampleCQRSApplication.DTO.TournamentTeam", null)
                        .WithMany("Teams")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.Tournament", b =>
                {
                    b.HasOne("SampleCQRSApplication.DTO.TournamentRound", null)
                        .WithMany("Tournaments")
                        .HasForeignKey("TournamentId");

                    b.HasOne("SampleCQRSApplication.DTO.TournamentSeason", null)
                        .WithMany("Tournaments")
                        .HasForeignKey("TournamentId");

                    b.HasOne("SampleCQRSApplication.DTO.TournamentTeam", null)
                        .WithMany("Tournaments")
                        .HasForeignKey("TournamentId");
                });

            modelBuilder.Entity("SampleCQRSApplication.Authentication.User", b =>
                {
                    b.Navigation("Bets");

                    b.Navigation("SendMails");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.Round", b =>
                {
                    b.Navigation("Matches");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.Team", b =>
                {
                    b.Navigation("HomeMatches");

                    b.Navigation("VisitingMatches");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.TournamentRound", b =>
                {
                    b.Navigation("Rounds");

                    b.Navigation("Tournaments");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.TournamentSeason", b =>
                {
                    b.Navigation("Seasons");

                    b.Navigation("Tournaments");
                });

            modelBuilder.Entity("SampleCQRSApplication.DTO.TournamentTeam", b =>
                {
                    b.Navigation("Teams");

                    b.Navigation("Tournaments");
                });
#pragma warning restore 612, 618
        }
    }
}
