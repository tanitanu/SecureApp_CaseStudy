using BlazorCARS.HealthShield.WebApp.Model.DTO;

namespace BlazorCARS.HealthShield.WebApp.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(UserDTO userDTO);

    }
}
