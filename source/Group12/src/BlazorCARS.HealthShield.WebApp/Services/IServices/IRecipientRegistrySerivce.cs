using BlazorCARS.HealthShield.WebApp.Model.DTO;

namespace BlazorCARS.HealthShield.WebApp.Services.IServices
{
    public interface IRecipientRegistrySerivce
    {
        Task<T> CreateAsync<T>(RecipientRegistryDTO CreateDto, string token);
    }
}
