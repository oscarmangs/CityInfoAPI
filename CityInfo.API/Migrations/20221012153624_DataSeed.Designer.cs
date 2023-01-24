﻿// <auto-generated />
using System;
using CityInfo.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CityInfo.API.Migrations
{
    [DbContext(typeof(CityInfoContext))]
    [Migration("20221012153624_DataSeed")]
    partial class DataSeed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("CityInfo.API.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "the one with the big park",
                            Name = "new york city"
                        },
                        new
                        {
                            Id = 2,
                            Description = "the one with the cathedral that was never finished",
                            Name = "Antwerp"
                        },
                        new
                        {
                            Id = 3,
                            Description = "the one with the big tower",
                            Name = "paris"
                        });
                });

            modelBuilder.Entity("CityInfo.API.Entities.PointOfInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int?>("PointOfInterestId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("PointOfInterestId");

                    b.ToTable("PointsOfInterest");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = 1,
                            Description = "the most visited urban park",
                            Name = "central park"
                        },
                        new
                        {
                            Id = 2,
                            CityId = 1,
                            Description = "the buildning spider man hangs out in",
                            Name = "empire state buildning"
                        },
                        new
                        {
                            Id = 3,
                            CityId = 2,
                            Description = "the cathedral",
                            Name = "cathedral"
                        },
                        new
                        {
                            Id = 4,
                            CityId = 3,
                            Description = "the tower made of iron that no one likes",
                            Name = "eifel tower"
                        },
                        new
                        {
                            Id = 5,
                            CityId = 3,
                            Description = "that museeum housing the famous painting",
                            Name = "the louvre"
                        });
                });

            modelBuilder.Entity("CityInfo.API.Entities.PointOfInterest", b =>
                {
                    b.HasOne("CityInfo.API.Entities.City", "City")
                        .WithMany("PointsOfInterest")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CityInfo.API.Entities.PointOfInterest", null)
                        .WithMany("PointsOfInterest")
                        .HasForeignKey("PointOfInterestId");

                    b.Navigation("City");
                });

            modelBuilder.Entity("CityInfo.API.Entities.City", b =>
                {
                    b.Navigation("PointsOfInterest");
                });

            modelBuilder.Entity("CityInfo.API.Entities.PointOfInterest", b =>
                {
                    b.Navigation("PointsOfInterest");
                });
#pragma warning restore 612, 618
        }
    }
}
