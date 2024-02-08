using BlazorCARS.HealthShield.Utility;
using BlazorCARS.HealthShield.Utility.Response;
using BlazorCARS.HealthShield.WebApp.Model.DTO;
using BlazorCARS.HealthShield.WebApp.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Data;

namespace BlazorCARS.HealthShield.WebApp.Pages.UserRole
{
    public class IndexModel : PageModel
    {
        private readonly IUserRoleSerivce _serivce;
        public List<UserRoleDTO> roleList { get; set; }

        public IndexModel(IUserRoleSerivce userRoleSerivce)
        {
            _serivce = userRoleSerivce;
        }
        public async Task OnGet()
        {
            string token = HttpContext.Session.GetString(SessionData.Token);

            var response = await _serivce.GetAllAsync<APIResponse>(10, 1, token);
            if (response != null && response.IsSuccess)
            {
                roleList = JsonConvert.DeserializeObject<List<UserRoleDTO>>(Convert.ToString(response.Result));
            }
        }
    }
}
