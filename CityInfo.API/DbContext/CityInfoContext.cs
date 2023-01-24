using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {

        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                
                new City("new york city")
                {
                    Id = 1,
                    Description = "the one with the big park"
                },
                new City("Antwerp")
                {
                    Id = 2,
                    Description = "the one with the cathedral that was never finished"
                },
                new City("paris")
                {
                    Id = 3,
                    Description = "the one with the big tower"
                });

            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("central park")
                {
                    Id = 1, 
                    CityId = 1, 
                    Description = "the most visited urban park"
                },
                new PointOfInterest("empire state buildning")
                {
                    Id = 2,
                    CityId = 1,
                    Description = "the buildning spider man hangs out in"
                },
                new PointOfInterest("cathedral")
                {
                    Id = 3,
                    CityId = 2,
                    Description = "the cathedral"
                },
                new PointOfInterest("eifel tower")
                {
                    Id = 4,
                    CityId = 3,
                    Description = "the tower made of iron that no one likes"
                },
                new PointOfInterest("the louvre")
                {
                    Id = 5,
                    CityId = 3,
                    Description = "that museeum housing the famous painting"
                });

            base.OnModelCreating(modelBuilder);
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=CityInfo.db}"); 

    }
}
