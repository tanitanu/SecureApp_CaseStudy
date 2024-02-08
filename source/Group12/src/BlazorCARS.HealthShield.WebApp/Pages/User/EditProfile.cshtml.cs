using BlazorCARS.HealthShield.DataObject.DTO;
using BlazorCARS.HealthShield.Utility;
using BlazorCARS.HealthShield.Utility.Response;
using BlazorCARS.HealthShield.WebApp.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace BlazorCARS.HealthShield.WebApp.Pages.user
{
    public class EditProfileModel : PageModel
    {
        private readonly IStateService _stateSerivce;
        private readonly ICountryService _countrySerivce;
        private readonly IRecipientService _serivce;

        public string Token { get; set; }
        public string ErrorMessage { get; set; }

        private List<StateDTO> StateList { get; set; }
        private List<CountryDTO> CountryList { get; set; }
        public SelectList StateOptions { get; set; }
        public SelectList CountryOptions { get; set; }

        [BindProperty]
        public Model.DTO.RecipientProfileUpdateDTO RecipientUpdate { get; set; }

        public EditProfileModel(IRecipientService recipientService, ICountryService countryService, IStateService stateService)
        {
            this._stateSerivce = stateService;
            this._countrySerivce = countryService;
            this._serivce = recipientService;
            this.RecipientUpdate = new Model.DTO.RecipientProfileUpdateDTO();
        }

        private async Task LoadMasters()
        {
            var responseCountry = await _countrySerivce.GetAllAsync<APIResponse>(10, 1, string.Empty);
            if (responseCountry != null && responseCountry.IsSuccess)
            {
                this.CountryList = JsonConvert.DeserializeObject<List<CountryDTO>>(Convert.ToString(responseCountry.Result));
                this.CountryOptions = new SelectList(this.CountryList, "CountryId", "Name");
            }

            var responseState = await _stateSerivce.GetAllAsync<APIResponse>(40, 1, string.Empty);
            if (responseState != null && responseState.IsSuccess)
            {
                this.StateList = JsonConvert.DeserializeObject<List<StateDTO>>(Convert.ToString(responseState.Result));
                this.StateOptions = new SelectList(this.StateList, "StateId", "Name");
            }
        }

        public async Task<IActionResult> OnGet()
        {

            if ((string.IsNullOrEmpty(HttpContext.Session.GetInt32(SessionData.Discrimination).ToString()))
                || (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionData.UserName))))
            {
                Redirect("/Logout");
            }
            else
            {
                await this.LoadMasters();
                int id = int.Parse(HttpContext.Session.GetInt32(SessionData.Discrimination).ToString());
                this.Token = HttpContext.Session.GetString(SessionData.Token);

                var response = await _serivce.GetAsync<Model.DTO.APIResponseDTO>(id, this.Token);
                if (response != null && response.IsSuccess)
                {
                    this.RecipientUpdate = JsonConvert.DeserializeObject<Model.DTO.RecipientProfileUpdateDTO>(response.Result.ToString());
                }
                else
                {
                    var result = response.ErrorMessages;
                    int index = 1;
                    foreach (var errMsg in result)
                    {
                        ModelState.AddModelError($"Err{index}", errMsg);
                        index++;
                    }
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.Token = HttpContext.Session.GetString(SessionData.Token);
                    this.RecipientUpdate.UpdatedUser = HttpContext.Session.GetString(SessionData.UserName);

                    var response = await _serivce.UpdateProfileAsync<Model.DTO.APIResponseDTO>(this.RecipientUpdate.RecipientId, this.RecipientUpdate, this.Token);
                    if (response != null && response.IsSuccess)
                    {
                        return Redirect("/User/ViewProfile");
                    }
                    else
                    {
                        var result = response.ErrorMessages;
                        int index = 1;
                        foreach (var errMsg in result)
                        {
                            ModelState.AddModelError($"Err{index}", errMsg);
                            index++;
                        }

                        //await this.LoadMasters();
                    }
                }
                else
                {
                    ModelState.AddModelError("Err1", "Please enter mandatory fields");
                    //await this.LoadMasters();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Exception: {ex.Message}";
                ModelState.AddModelError("Err1", ErrorMessage);
                await this.LoadMasters();
            }
            await this.LoadMasters();
            return Page();
        }
    }
}
