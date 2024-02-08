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
    public class ViewHospitalModel : PageModel
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
        public Model.DTO.HospitalDTO Hospital { get; set; }

        public ViewHospitalModel(IHospitalService hospitalService, ICountryService countryService, IStateService stateService)
        {
            this._serivce = hospitalService;
            this._stateSerivce = stateService;
            this._countrySerivce = countryService;
            this.Hospital = new Model.DTO.HospitalDTO();
        }

        private async Task LoadMasters()
        {
            var responseCountry = await _countrySerivce.GetAllAsync<APIResponse>(10, 1, string.Empty);//TODO: Pass token once session implemented
            if (responseCountry != null && responseCountry.IsSuccess)
            {
                this.CountryList = JsonConvert.DeserializeObject<List<CountryDTO>>(Convert.ToString(responseCountry.Result));
                this.CountryOptions = new SelectList(this.CountryList, "CountryId", "Name");
            }

            var responseState = await _stateSerivce.GetAllAsync<APIResponse>(40, 1, string.Empty);//TODO: Pass token once session implemented
            if (responseState != null && responseState.IsSuccess)
            {
                this.StateList = JsonConvert.DeserializeObject<List<StateDTO>>(Convert.ToString(responseState.Result));
                this.StateOptions = new SelectList(this.StateList, "StateId", "Name");
            }
        }

        public async Task<IActionResult> OnGet()
        {
            this.Token = HttpContext.Session.GetString(SessionData.Token);

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
                    this.Hospital = JsonConvert.DeserializeObject<Model.DTO.HospitalDTO>(response.Result.ToString());
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
                    await this.LoadMasters();
                }
            }

            return Page();

        }
    }
}
