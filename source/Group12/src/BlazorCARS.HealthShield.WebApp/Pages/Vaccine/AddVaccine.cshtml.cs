using BlazorCARS.HealthShield.WebApp.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorCARS.HealthShield.WebApp.Pages.User
{
    public class AddVaccineModel : PageModel
    {
        private readonly IVaccineService _serivce;
        public string ErrorMessage { get; set; }

        [BindProperty]
        public Model.DTO.VaccineDTO Vaccine { get; set; }

        public AddVaccineModel(IVaccineService vaccineService)
        {
            this._serivce = vaccineService;
        }

        public void OnGet()
        {


        }
    }
}

