using BlazorCARS.HealthShield.WebApp.Model.DTO;

namespace BlazorCARS.HealthShield.WebApp.Services.IServices
{
    public interface IHospitalRegistrySerivce
    {
        Task<T> CreateAsync<T>(HospitalRegistryDTO CreateDto, string token);
    }
}
