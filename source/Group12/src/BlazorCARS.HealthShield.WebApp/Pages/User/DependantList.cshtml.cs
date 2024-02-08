using BlazorCARS.HealthShield.DataObject.DTO;
using BlazorCARS.HealthShield.Utility;
using BlazorCARS.HealthShield.Utility.Response;
using BlazorCARS.HealthShield.WebApp.Model;
using BlazorCARS.HealthShield.WebApp.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BlazorCARS.HealthShield.WebApp.Pages.User
{
    public class DependantListModel : PageModel
    {
        private readonly IRecipientService _serivce;
        public string ErrorMessage { get; set; }
        public List<RecipientDTO> RecipientList { get; set; }

        public DependantListModel(IRecipientService recipientService)
        {
            this._serivce = recipientService;
        }

        public async Task<IActionResult> OnGet()
        {
            int id = 0;
            if (string.IsNullOrEmpty(HttpContext.Session.GetInt32(SessionData.Discrimination).ToString()))
                return Redirect("/Logout");
            else
                id = int.Parse(HttpContext.Session.GetInt32(SessionData.Discrimination).ToString());

            string token = HttpContext.Session.GetString(SessionData.Token);

            var response = await _serivce.GetAllDepandantsAsync<APIResponse>(id, token);
            if (response != null && response.IsSuccess)
            {
                this.RecipientList = JsonConvert.DeserializeObject<List<RecipientDTO>>(Convert.ToString(response.Result));
            }

            return Page();
        }
    }
}
