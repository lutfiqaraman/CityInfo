using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCities();
        Task<City?> GetCityById(int cityId, bool includePointsOfInterest);
        Task<bool> IsCityExist(int cityId);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCity(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCity(int cityId, int pointOfInterestId);
    }
}
