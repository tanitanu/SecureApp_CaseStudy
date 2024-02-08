using BlazorCARS.HealthShield.DataObject.DTO;

namespace BlazorCARS.HealthShield.WebApp.Services.IServices
{
    public interface IStateService
    {
        Task<T> GetAllAsync<T>(int pageSize, int pageNumber, string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(StateCreateDTO CreateDto, string token);
        Task<T> UpdateAsync<T>(int id, StateUpdateDTO UpdateDto, string token);
    }
}
