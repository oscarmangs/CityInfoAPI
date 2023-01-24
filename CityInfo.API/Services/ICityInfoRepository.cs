using CityInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        public Task<IEnumerable<City>> GetCitiesAsync();

        public Task<(IEnumerable<City>, PaginationMetaData)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize);

        public Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);

        public Task<bool> CityExistsAsync(int cityId);
        public Task<PointOfInterest> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);

        public Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);

        public Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);

        public Task<bool> SaveChangesAsync();

        void DeletePointOfInterest(PointOfInterest pointOfInterest);
    }
}
