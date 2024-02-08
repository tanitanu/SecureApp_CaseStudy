//using BlazorCARS.HealthShield.DataObject.DTO;
using BlazorCARS.HealthShield.WebApp.Model.DTO;

namespace BlazorCARS.HealthShield.WebApp.Services.IServices
{
    public interface IHospitalService
    {
        Task<T> GetAllAsync<T>(int pageSize, int pageNumber, string token);

        Task<T> GetAsync<T>(int id, string token);

        Task<T> CreateAsync<T>(HospitalCreateDTO CreateDto, string token);

        Task<T> UpdateAsync<T>(int id, HospitalUpdateDTO UpdateDto, string token);
    }
}
