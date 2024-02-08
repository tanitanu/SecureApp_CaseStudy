using BlazorCARS.HealthShield.WebApp.Model.DTO;

namespace BlazorCARS.HealthShield.WebApp.Services.IServices
{
    public interface IVaccineRegistrationService
    {
        Task<T> CreateAsync<T>(VaccineRegistrationCreateDTO CreateDto, string token);
        Task<T> GetAllAsync<T>(int pageSize, int pageNumber, string token);
        Task<T> GetActiveAppointmentByHospitalAsync<T>(int id, string token);
        Task<T> GetActiveappoinmentAsync<T>(int id, string token);
    }
}
