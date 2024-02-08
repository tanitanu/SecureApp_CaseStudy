using BlazorCARS.HealthShield.DataObject.DTO;
using BlazorCARS.HealthShield.Utility;
using BlazorCARS.HealthShield.Utility.Response;
using BlazorCARS.HealthShield.WebApp.Model.DTO;
using BlazorCARS.HealthShield.WebApp.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace BlazorCARS.HealthShield.WebApp.Pages.hospital
{
    public class HospitalRegistrationModel : PageModel
    {
        private readonly IStateService _stateSerivce;
        private readonly ICountryService _countrySerivce;
        private readonly IHospitalRegistrySerivce _serivce;

        public string ErrorMessage { get; set; }

        private List<StateDTO> StateList { get; set; }
        private List<CountryDTO> CountryList { get; set; }

        public SelectList StateOptions { get; set; }
        public SelectList CountryOptions { get; set; }

        [BindProperty]
        public Model.DTO.HospitalRegistryDTO HospitalCreate { get; set; }

        public HospitalRegistrationModel(IHospitalRegistrySerivce hospitalSerivce, ICountryService countryService, IStateService stateService)
        {
            this._serivce = hospitalSerivce;
            this._stateSerivce = stateService;
            this._countrySerivce = countryService;
            this.HospitalCreate = new Model.DTO.HospitalRegistryDTO();
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
            await this.LoadMasters();
        }

        public async Task<IActionResult> OnPostAsync(Model.DTO.HospitalRegistryDTO HospitalCreate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!(HospitalCreate.Password.Equals(this.HospitalCreate.ConfirmPassword)))
                    {
                        ModelState.AddModelError("RecipientCreate.ConfirmPassword", "Password and Confirm Password does not match.");
                    }
                    this.HospitalCreate.CreatedUser = "Registration";
                    string token = HttpContext.Session.GetString(SessionData.Token);

                    var response = await _serivce.CreateAsync<Model.DTO.APIResponseDTO>(this.HospitalCreate, token);

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

