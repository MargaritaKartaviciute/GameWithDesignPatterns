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
    [Migration("20191014192009_virtuals_added")]
    partial class virtuals_added
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

                    b.Property<int>("PowerFor");

                    b.HasKey("Id");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("backend.Models.Map", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaxX");

                    b.Property<int>("MaxY");

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

            modelBuilder.Entity("backend.Models.MapObjects.PlayerCutItems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CoinsWorth");

                    b.Property<int>("MapObjectId");

                    b.Property<int>("PlayerId");

                    b.HasKey("Id");

                    b.HasIndex("MapObjectId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerCutItem");
                });

            modelBuilder.Entity("backend.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Energy");

                    b.Property<int>("LifeAmount");

                    b.Property<int?>("MapId");

                    b.Property<int>("Score");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("MapId");

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

            modelBuilder.Entity("backend.Models.MapObjects.PlayerCutItems", b =>
                {
                    b.HasOne("backend.Models.MapObject", "MapObject")
                        .WithMany()
                        .HasForeignKey("MapObjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("backend.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("backend.Models.Player", b =>
                {
                    b.HasOne("backend.Models.Map", "Map")
                        .WithMany("Players")
                        .HasForeignKey("MapId");
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
#pragma warning restore 612, 618
        }
    }
}
