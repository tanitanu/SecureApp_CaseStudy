using BlazorCARS.HealthShield.WebApp.Model.DTO;

namespace BlazorCARS.HealthShield.WebApp.Services.IServices
{
    public interface IRecipientService
    {
        Task<T> GetAllAsync<T>(int pageSize, int pageNumber, string token);
        Task<T> GetAllDepandantsAsync<T>(int id, string token);
     
        Task<T> GetAsync<T>(int id, string token);
        
        Task<T> CreateAsync<T>(RecipientCreateDTO CreateDto, string token);
        
        Task<T> UpdateAsync<T>(int id, RecipientUpdateDTO UpdateDto, string token);

        Task<T> UpdateProfileAsync<T>(int id, RecipientProfileUpdateDTO UpdateDto, string token);

        //Task<T> DeleteAsync<T>(int id, string DeletedUSer, string token);
    }
}
