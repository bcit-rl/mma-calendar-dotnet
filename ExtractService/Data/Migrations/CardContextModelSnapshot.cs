﻿// <auto-generated />
using System;
using DBClass.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExtractService.Data.Migrations
{
    [DbContext(typeof(CardContext))]
    partial class CardContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DBClass.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("EventDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EventName")
                        .HasColumnType("longtext");

                    b.Property<string>("ShortName")
                        .HasColumnType("longtext");

                    b.Property<int?>("VenueId")
                        .HasColumnType("int");

                    b.HasKey("EventId");

                    b.HasIndex("VenueId");

                    b.ToTable("Events", (string)null);
                });

            modelBuilder.Entity("DBClass.Models.Fight", b =>
                {
                    b.Property<int>("FightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CardSegment")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("DisplayClock")
                        .HasColumnType("longtext");

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<int?>("FighterAId")
                        .HasColumnType("int");

                    b.Property<int?>("FighterBId")
                        .HasColumnType("int");

                    b.Property<int?>("MatchNumber")
                        .HasColumnType("int");

                    b.Property<string>("Method")
                        .HasColumnType("longtext");

                    b.Property<string>("MethodDescription")
                        .HasColumnType("longtext");

                    b.Property<int?>("Round")
                        .HasColumnType("int");

                    b.Property<string>("WeightClass")
                        .HasColumnType("longtext");

                    b.Property<string>("Winner")
                        .HasColumnType("longtext");

                    b.HasKey("FightId");

                    b.HasIndex("EventId");

                    b.HasIndex("FighterAId");

                    b.HasIndex("FighterBId");

                    b.ToTable("Fights", (string)null);
                });

            modelBuilder.Entity("DBClass.Models.FightHistory", b =>
                {
                    b.Property<int>("FighterId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Event")
                        .HasColumnType("longtext");

                    b.Property<string>("Method")
                        .HasColumnType("longtext");

                    b.Property<string>("Opponent")
                        .HasColumnType("longtext");

                    b.Property<string>("Result")
                        .HasColumnType("longtext");

                    b.Property<string>("Round")
                        .HasColumnType("longtext");

                    b.Property<string>("Time")
                        .HasColumnType("longtext");

                    b.HasKey("FighterId", "Date");

                    b.ToTable("FightHistory", (string)null);
                });

            modelBuilder.Entity("DBClass.Models.Fighter", b =>
                {
                    b.Property<int>("FighterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Citizenship")
                        .HasColumnType("longtext");

                    b.Property<int?>("Draws")
                        .HasColumnType("int");

                    b.Property<int?>("FightId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .HasColumnType("longtext");

                    b.Property<string>("Headshot")
                        .HasColumnType("longtext");

                    b.Property<int?>("Height")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("LeftStance")
                        .HasColumnType("longtext");

                    b.Property<int?>("Losses")
                        .HasColumnType("int");

                    b.Property<string>("NickName")
                        .HasColumnType("longtext");

                    b.Property<int?>("NoContests")
                        .HasColumnType("int");

                    b.Property<string>("RightStance")
                        .HasColumnType("longtext");

                    b.Property<int?>("Weight")
                        .HasColumnType("int");

                    b.Property<int?>("Wins")
                        .HasColumnType("int");

                    b.HasKey("FighterId");

                    b.HasIndex("FightId");

                    b.ToTable("Fighters", (string)null);
                });

            modelBuilder.Entity("DBClass.Models.Venue", b =>
                {
                    b.Property<int>("VenueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.Property<bool?>("Indoor")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("State")
                        .HasColumnType("longtext");

                    b.HasKey("VenueId");

                    b.ToTable("Venues", (string)null);
                });

            modelBuilder.Entity("DBClass.Models.Event", b =>
                {
                    b.HasOne("DBClass.Models.Venue", "Venue")
                        .WithMany("Events")
                        .HasForeignKey("VenueId");

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("DBClass.Models.Fight", b =>
                {
                    b.HasOne("DBClass.Models.Event", "Event")
                        .WithMany("Fights")
                        .HasForeignKey("EventId");

                    b.HasOne("DBClass.Models.Fighter", "FighterA")
                        .WithMany()
                        .HasForeignKey("FighterAId");

                    b.HasOne("DBClass.Models.Fighter", "FighterB")
                        .WithMany()
                        .HasForeignKey("FighterBId");

                    b.Navigation("Event");

                    b.Navigation("FighterA");

                    b.Navigation("FighterB");
                });

            modelBuilder.Entity("DBClass.Models.Fighter", b =>
                {
                    b.HasOne("DBClass.Models.Fight", null)
                        .WithMany("Fighters")
                        .HasForeignKey("FightId");
                });

            modelBuilder.Entity("DBClass.Models.Event", b =>
                {
                    b.Navigation("Fights");
                });

            modelBuilder.Entity("DBClass.Models.Fight", b =>
                {
                    b.Navigation("Fighters");
                });

            modelBuilder.Entity("DBClass.Models.Venue", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
