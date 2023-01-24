using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }

        //public static CitiesDataStore Current { get; } = new CitiesDataStore();    

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "new york city",
                    Description = "the one with that big park",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto() { Id = 1, Name = "Central Park", Description = "The big park" },
                        new PointOfInterestDto() {Id = 2, Name = "Empire State Building", Description = "The Big Building"}
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "antwerp",
                    Description = "the one with the cathedral that was never really finished",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto() { Id = 3, Name = "Antwerp station", Description = "The Station" },
                        new PointOfInterestDto() {Id = 4, Name = "Antwerp Garden", Description = "The Garden"}
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "paris",
                    Description = "the one with that big tower",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto() { Id = 5, Name = "Eiffel Tower", Description = "The iron tower" },
                        new PointOfInterestDto() {Id = 6, Name = "The Louvre", Description = "The Museum"}
                    }
                }
            }; 
        }
    }
}
