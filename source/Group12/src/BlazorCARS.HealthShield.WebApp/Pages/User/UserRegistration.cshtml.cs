using BlazorCARS.HealthShield.DataObject.DTO;
using BlazorCARS.HealthShield.Utility;
using BlazorCARS.HealthShield.Utility.Response;
using BlazorCARS.HealthShield.WebApp.Model.DTO;
using BlazorCARS.HealthShield.WebApp.Services;
using BlazorCARS.HealthShield.WebApp.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlazorCARS.HealthShield.WebApp.Pages.user
{
    public class UserRegistrationModel : PageModel
    {
        private readonly IStateService _stateSerivce;
        private readonly ICountryService _countrySerivce;
        private readonly IRecipientRegistrySerivce _serivce;

        public string ErrorMessage { get; set; }

        private List<StateDTO> StateList { get; set; }
        private List<CountryDTO> CountryList { get; set; }

        public SelectList StateOptions { get; set; }
        public SelectList CountryOptions { get; set; }

        [BindProperty]
        public Model.DTO.RecipientRegistryDTO RecipientCreate { get; set; }

        public UserRegistrationModel(IRecipientRegistrySerivce recipientRegistrySerivce, ICountryService countryService, IStateService stateService)
        {
            this._stateSerivce = stateService;
            this._countrySerivce = countryService;
            this._serivce = recipientRegistrySerivce;
            this.RecipientCreate = new Model.DTO.RecipientRegistryDTO();
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

        public async Task OnGet()
        {
            this.RecipientCreate.DOB = DateTime.Now;
            await this.LoadMasters();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!(this.RecipientCreate.Password.Equals(this.RecipientCreate.ConfirmPassword)))
                    {
                        ModelState.AddModelError("RecipientCreate.ConfirmPassword", "Password and Confirm Password does not match.");
                    }
                    this.RecipientCreate.CreatedUser = "Registration";
                    string token = HttpContext.Session.GetString(SessionData.Token);

                    var response = await _serivce.CreateAsync<Model.DTO.APIResponseDTO>(this.RecipientCreate, token);

                    if (response != null && response.IsSuccess)
                    {
                        return Redirect("/Login");
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
                else
                {
                    ModelState.AddModelError("Err1", "Please enter mandatory fields");
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
