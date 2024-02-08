using BlazorCARS.HealthShield.Utility.Response;
using BlazorCARS.HealthShield.WebApp.Model;
/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.WebApp.Services.IServices
{
    public interface IBaseService
    {
        public APIResponse response { get; set; }
        Task<T> SendAsync<T>(APIRequest reqeust);
    }
}
