using BlazorCARS.HealthShield.WebApp.Model.DTO;
/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.WebApp.Services.IServices
{
    public interface IUserRoleSerivce
    {
        Task<T> GetAllAsync<T>(int pageSize, int pageNumber, string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(UserRoleCreateDTO CreateDto, string token);
        Task<T> UpdateAsync<T>(int id, UserRoleUpdateDTO UpdateDto, string token);
        Task<T> DeleteAsync<T>(int id, string DeletedUSer, string token);
    }
}
