using BlazorCARS.HealthShield.DataObject.DTO;

namespace BlazorCARS.HealthShield.WebApp.Services.IServices
{
    public interface IVaccineScheduleService
    {
        Task<T> GetAllAsync<T>(int pageSize, int pageNumber, string token);

        Task<T> GetAsync<T>(int id, string token);

        Task<T> CreateAsync<T>(VaccineScheduleCreateDTO CreateDto, string token);
        Task<T> UpdateAsync<T>(int id, VaccineScheduleUpdateDTO UpdateDto, string token);
        Task<T> DeleteAsync<T>(int id, string DeletedUSer, string token);
    }
}
