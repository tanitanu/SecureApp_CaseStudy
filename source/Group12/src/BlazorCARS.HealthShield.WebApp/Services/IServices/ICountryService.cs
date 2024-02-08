using BlazorCARS.HealthShield.DataObject.DTO;

namespace BlazorCARS.HealthShield.WebApp.Services.IServices
{
    public interface ICountryService
    {
        Task<T> GetAllAsync<T>(int pageSize, int pageNumber, string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(CountryCreateDTO CreateDto, string token);
        Task<T> UpdateAsync<T>(int id, CountryUpdateDTO UpdateDto, string token);
    }
}
