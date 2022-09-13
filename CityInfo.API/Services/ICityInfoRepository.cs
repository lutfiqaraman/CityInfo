using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCities();
        Task<City?> GetCityById(int cityId);
        Task <IEnumerable<PointOfInterest>> GetPointsOfInterestForCity(int cityId);
    }
}
