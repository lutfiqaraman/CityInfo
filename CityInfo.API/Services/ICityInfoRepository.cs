using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCities();
        Task<IEnumerable<City>> GetCities(string? name);
        Task<City?> GetCityById(int cityId, bool includePointsOfInterest);
        Task<bool> IsCityExist(int cityId);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCity(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCity(int cityId, int pointOfInterestId);
        Task AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);
        void DeletePointOfInterest(PointOfInterest pointOfInterest);
        Task<bool> SaveChanges();
    }
}
