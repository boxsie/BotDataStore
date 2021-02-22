﻿// <auto-generated />
using System;
using BotData.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BotData.Data.Migrations
{
    [DbContext(typeof(BotDataContext))]
    [Migration("20210222153944_MigrateUserId")]
    partial class MigrateUserId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("BotData.Data.Entity.BotUser.User", b =>
                {
                    b.Property<long>("DiscordId")
                        .HasColumnType("bigint");

                    b.Property<string>("EntranceSound")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DiscordId");

                    b.HasIndex("Name");

                    b.ToTable("User");
                });

            modelBuilder.Entity("BotData.Data.Entity.Game.GuessGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CorrectAnswer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("DiscordId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("FinishedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("GameName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("DiscordId");

                    b.ToTable("GuessGame");
                });

            modelBuilder.Entity("BotData.Data.Entity.Game.GuessGameAttempt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Attempt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("DiscordId")
                        .HasColumnType("bigint");

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DiscordId");

                    b.HasIndex("GameId");

                    b.ToTable("GuessGameAttempt");
                });

            modelBuilder.Entity("BotData.Data.Entity.GeoSniff.Location", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Area")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<double>("Lat")
                        .HasColumnType("double precision");

                    b.Property<double>("Long")
                        .HasColumnType("double precision");

                    b.Property<double>("Radius")
                        .HasColumnType("double precision");

                    b.Property<string>("SubArea")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Country");

                    b.ToTable("GeoSniffLocation");
                });

            modelBuilder.Entity("BotData.Data.Entity.Game.GuessGame", b =>
                {
                    b.HasOne("BotData.Data.Entity.BotUser.User", "User")
                        .WithMany("GuessGames")
                        .HasForeignKey("DiscordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BotData.Data.Entity.Game.GuessGameAttempt", b =>
                {
                    b.HasOne("BotData.Data.Entity.BotUser.User", "User")
                        .WithMany("GuessGameAttempts")
                        .HasForeignKey("DiscordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BotData.Data.Entity.Game.GuessGame", "Game")
                        .WithMany("Attempts")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BotData.Data.Entity.BotUser.User", b =>
                {
                    b.Navigation("GuessGameAttempts");

                    b.Navigation("GuessGames");
                });

            modelBuilder.Entity("BotData.Data.Entity.Game.GuessGame", b =>
                {
                    b.Navigation("Attempts");
                });
#pragma warning restore 612, 618
        }
    }
}