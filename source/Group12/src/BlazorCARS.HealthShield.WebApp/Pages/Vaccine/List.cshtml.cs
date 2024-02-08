using BlazorCARS.HealthShield.DataObject.DTO;
using BlazorCARS.HealthShield.Utility;
using BlazorCARS.HealthShield.Utility.Response;
using BlazorCARS.HealthShield.WebApp.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BlazorCARS.HealthShield.WebApp.Pages.Vaccine
{
    public class List : PageModel
    {
        private readonly IVaccineService _serivce;
        public string ErrorMessage { get; set; }
        public List<VaccineDTO> VaccineList { get; set; }

        public List(IVaccineService VaccineService)
        {
            this._serivce = VaccineService;
        }
        public async Task<IActionResult> OnGet()
        {

            string token = HttpContext.Session.GetString(SessionData.Token);

            var response = await _serivce.GetAllAsync<APIResponse>(100, 1, token);
            if (response != null && response.IsSuccess)
            {
                this.VaccineList = JsonConvert.DeserializeObject<List<VaccineDTO>>(Convert.ToString(response.Result));
            }

            return Page();
        }
        /*public async Task OnGet()
        {
            var response = await _serivce.GetAllAsync<APIResponse>(10, 1, string.Empty);//TODO: Pass token once session implemented
            if (response != null && response.IsSuccess)
            {
                this.VaccineList = JsonConvert.DeserializeObject<List<VaccineDTO>>(Convert.ToString(response.Result));
            }

            

        }*/
    }
}
