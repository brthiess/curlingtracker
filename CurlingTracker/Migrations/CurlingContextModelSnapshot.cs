﻿// <auto-generated />
using System;
using CurlingTracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CurlingTracker.Migrations
{
    [DbContext(typeof(CurlingContext))]
    partial class CurlingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846");

            modelBuilder.Entity("CurlingTracker.Models.Draw", b =>
                {
                    b.Property<Guid>("DrawId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("DisplayName")
                        .IsRequired();

                    b.Property<Guid>("EventId");

                    b.Property<bool>("IsOverAndFullyParsed");

                    b.HasKey("DrawId");

                    b.HasIndex("EventId");

                    b.ToTable("Draws");
                });

            modelBuilder.Entity("CurlingTracker.Models.Event", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CZId");

                    b.Property<DateTime>("EndDate");

                    b.Property<bool>("IsOverAndFullyParsed");

                    b.Property<string>("Location")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("StartDate");

                    b.HasKey("EventId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("CurlingTracker.Models.EventType", b =>
                {
                    b.Property<Guid>("EventTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("EventId");

                    b.Property<int>("Gender");

                    b.Property<int>("NumberOfEnds");

                    b.Property<int>("NumberOfPlayers");

                    b.Property<int>("teamType");

                    b.HasKey("EventTypeId");

                    b.HasIndex("EventId")
                        .IsUnique();

                    b.ToTable("EventType");
                });

            modelBuilder.Entity("CurlingTracker.Models.Game", b =>
                {
                    b.Property<Guid>("GameId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("DrawId");

                    b.Property<Guid>("EventId");

                    b.Property<bool>("IsFinal");

                    b.Property<bool>("IsOverAndFullyParsed");

                    b.Property<bool>("PercentagesAvailable");

                    b.HasKey("GameId");

                    b.HasIndex("DrawId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("CurlingTracker.Models.Linescore", b =>
                {
                    b.Property<Guid>("LinescoreId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DictionaryAsJson");

                    b.Property<Guid>("GameId");

                    b.Property<int>("NumberOfEnds");

                    b.HasKey("LinescoreId");

                    b.HasIndex("GameId")
                        .IsUnique();

                    b.ToTable("Linescore");
                });

            modelBuilder.Entity("CurlingTracker.Models.Player", b =>
                {
                    b.Property<Guid>("PlayerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<string>("Image");

                    b.Property<bool>("IsSkip");

                    b.Property<string>("LastName");

                    b.Property<Guid>("TeamId");

                    b.Property<int>("position");

                    b.HasKey("PlayerId");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("CurlingTracker.Models.Team", b =>
                {
                    b.Property<Guid>("TeamId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("GameId");

                    b.Property<string>("Name");

                    b.Property<int>("TeamType");

                    b.Property<int>("gender");

                    b.HasKey("TeamId");

                    b.HasIndex("GameId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("CurlingTracker.Models.Draw", b =>
                {
                    b.HasOne("CurlingTracker.Models.Event")
                        .WithMany("Draws")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CurlingTracker.Models.EventType", b =>
                {
                    b.HasOne("CurlingTracker.Models.Event")
                        .WithOne("Type")
                        .HasForeignKey("CurlingTracker.Models.EventType", "EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CurlingTracker.Models.Game", b =>
                {
                    b.HasOne("CurlingTracker.Models.Draw")
                        .WithMany("Games")
                        .HasForeignKey("DrawId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CurlingTracker.Models.Linescore", b =>
                {
                    b.HasOne("CurlingTracker.Models.Game")
                        .WithOne("Linescore")
                        .HasForeignKey("CurlingTracker.Models.Linescore", "GameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CurlingTracker.Models.Player", b =>
                {
                    b.HasOne("CurlingTracker.Models.Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CurlingTracker.Models.Team", b =>
                {
                    b.HasOne("CurlingTracker.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
