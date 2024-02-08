using BlazorCARS.HealthShield.DataObject.DTO;
using BlazorCARS.HealthShield.Utility;
using BlazorCARS.HealthShield.Utility.Response;
using BlazorCARS.HealthShield.WebApp.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace BlazorCARS.HealthShield.WebApp.Pages.hospital
{
    public class EditHospitalModel : PageModel
    {
        private readonly IStateService _stateSerivce;
        private readonly ICountryService _countrySerivce;
        private readonly IHospitalService _serivce;

        public string Token { get; set; }
        public string ErrorMessage { get; set; }

        private List<StateDTO> StateList { get; set; }
        private List<CountryDTO> CountryList { get; set; }
        public SelectList StateOptions { get; set; }
        public SelectList CountryOptions { get; set; }

        [BindProperty]
        public Model.DTO.HospitalUpdateDTO Hospital { get; set; }

        public EditHospitalModel(IHospitalService hospitalService, ICountryService countryService, IStateService stateService)
        {
            this._stateSerivce = stateService;
            this._countrySerivce = countryService;
            this._serivce = hospitalService;
            this.Hospital = new Model.DTO.HospitalUpdateDTO();
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
                    this.Hospital = JsonConvert.DeserializeObject<Model.DTO.HospitalUpdateDTO>(response.Result.ToString());
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
                    this.Hospital.UpdatedUser = HttpContext.Session.GetString(SessionData.UserName);
                    this.Token = HttpContext.Session.GetString(SessionData.Token);

                    var response = await _serivce.UpdateAsync<Model.DTO.APIResponseDTO>(this.Hospital.HospitalId, this.Hospital, this.Token);
                    if (response != null && response.IsSuccess)
                    {
                        return Redirect("/Hospital/ViewHospital");
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
