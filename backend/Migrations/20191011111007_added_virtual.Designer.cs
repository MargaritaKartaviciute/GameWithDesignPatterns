﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend.Data;

namespace backend.Migrations
{
    [DbContext(typeof(SalaContext))]
    [Migration("20191011111007_added_virtual")]
    partial class added_virtual
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("backend.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("Power");

                    b.HasKey("Id");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("backend.Models.Map", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Map");
                });

            modelBuilder.Entity("backend.Models.MapObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("MapId");

                    b.Property<int>("X");

                    b.Property<int>("Y");

                    b.HasKey("Id");

                    b.HasIndex("MapId");

                    b.ToTable("MapObject");

                    b.HasDiscriminator<string>("Discriminator").HasValue("MapObject");
                });

            modelBuilder.Entity("backend.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("backend.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("backend.Models.PlayerItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ItemId");

                    b.Property<int?>("PlayerId");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerItems");
                });

            modelBuilder.Entity("backend.Models.PlayerMatch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Energy");

                    b.Property<int>("LifeAmount");

                    b.Property<int?>("MatchId");

                    b.Property<string>("Name");

                    b.Property<int?>("PlayerId");

                    b.Property<int>("Score");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerMatch");
                });

            modelBuilder.Entity("backend.Models.Rock", b =>
                {
                    b.HasBaseType("backend.Models.MapObject");

                    b.HasDiscriminator().HasValue("Rock");
                });

            modelBuilder.Entity("backend.Models.Tree", b =>
                {
                    b.HasBaseType("backend.Models.MapObject");

                    b.HasDiscriminator().HasValue("Tree");
                });

            modelBuilder.Entity("backend.Models.Water", b =>
                {
                    b.HasBaseType("backend.Models.MapObject");

                    b.HasDiscriminator().HasValue("Water");
                });

            modelBuilder.Entity("backend.Models.MapObject", b =>
                {
                    b.HasOne("backend.Models.Map", "Map")
                        .WithMany("MapObjects")
                        .HasForeignKey("MapId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("backend.Models.PlayerItem", b =>
                {
                    b.HasOne("backend.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId");

                    b.HasOne("backend.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("backend.Models.PlayerMatch", b =>
                {
                    b.HasOne("backend.Models.Match", "Match")
                        .WithMany()
                        .HasForeignKey("MatchId");

                    b.HasOne("backend.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");
                });
#pragma warning restore 612, 618
        }
    }
}
