using BlazorCARS.HealthShield.WebApp.Model.DTO;

namespace BlazorCARS.HealthShield.WebApp.Services.IServices
{
    public interface IVaccineService
    {
        Task<T> GetAllAsync<T>(int pageSize, int pageNumber, string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(VaccineCreateDTO CreateDto, string token);
        Task<T> UpdateAsync<T>(int id, VaccineUpdateDTO UpdateDto, string token);
        //Task<T> DeleteAsync<T>(int id, string DeletedUSer, string token);

        //Task<T> GetAllVaccineAsync<T>(int id, string token);
    }
}
