using BlazorCARS.HealthShield.DataObject.DTO;
using BlazorCARS.HealthShield.Utility;
using BlazorCARS.HealthShield.Utility.Response;
using BlazorCARS.HealthShield.WebApp.Services.IServices;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BlazorCARS.HealthShield.WebApp.Pages.user
{
    public class ListModel : PageModel
    {
        private readonly IRecipientService _serivce;
        public string ErrorMessage { get; set; }
        public List<RecipientDTO> RecipientList { get; set; }

        public ListModel(IRecipientService recipientService)
        {
            this._serivce = recipientService;
        }

        public async Task OnGet()
        {
            string token = HttpContext.Session.GetString(SessionData.Token);

            var response = await _serivce.GetAllAsync<APIResponse>(10, 1, token);
            if (response != null && response.IsSuccess)
            {
                this.RecipientList = JsonConvert.DeserializeObject<List<RecipientDTO>>(Convert.ToString(response.Result));
            }
        }
    }
}
