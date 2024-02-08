
using BlazorCARS.HealthShield.DataObject.Enums;
using BlazorCARS.HealthShield.Utility;
using BlazorCARS.HealthShield.Utility.Response;
using BlazorCARS.HealthShield.WebApp.Model.DTO;
using BlazorCARS.HealthShield.WebApp.Services;
using BlazorCARS.HealthShield.WebApp.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlazorCARS.HealthShield.WebApp.Pages.user
{
    public class VaccineRegisterModel : PageModel
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public int Discrimination { get; set; }

        public string recipientName { get; set; }
        public int recipient { get; set; }
        public string ErrorMessage { get; set; }
        public Dose SelectedDose { get; set; }
        public List<SelectListItem> DoseOptions { get; set; }
        public List<DateTime> UniqueScheduleDates { get; set; } = new List<DateTime>(); // Change the type to a list of DateTime
        public List<string> UniqueTimeslots { get; set; } = new List<string>();
        public DateTime SelectedDate { get; set; } // Property to hold the selected date
        private readonly IHospitalService _serivce;
        private readonly IVaccineScheduleService _VaccineSerivce;
        private readonly IRecipientService _recipientService;
        private readonly IVaccineRegistrationService _vaccineRegistrationService;
        private List<HospitalDTO> hospitalList { get; set; }
        private List<RecipientDTO> recipientList { get; set; }
        public SelectList HospitalOptions { get; set; }
        public List<VaccineScheduleDTO> vaccineList { get; set; }
        [BindProperty]
        public Model.DTO.VaccineRegistrationCreateDTO VaccineRegistrationCreate { get; set; }

        public VaccineRegisterModel(IHospitalService hospitalSerivce, IVaccineScheduleService vaccineSerivce, IRecipientService recipientService, IVaccineRegistrationService vaccineRegistrationService)
        {
            _serivce = hospitalSerivce;
            _VaccineSerivce = vaccineSerivce;
            _recipientService = recipientService;
            this.hospitalList = new List<HospitalDTO>(); // Initialize the list
            this.vaccineList = new List<VaccineScheduleDTO>(); // Initialize the list
            this.recipientList = new List<RecipientDTO>();
            _vaccineRegistrationService = vaccineRegistrationService;
            this.VaccineRegistrationCreate = new Model.DTO.VaccineRegistrationCreateDTO();
        }
        private async Task LoadMasters(int id)
        {
            recipient = id;
            string token = HttpContext.Session.GetString(SessionData.Token);

            var responserecipient = await _recipientService.GetAsync<APIResponse>(id, token);
            if (responserecipient != null && responserecipient.IsSuccess)
            {
                RecipientDTO recipient1 = JsonConvert.DeserializeObject<RecipientDTO>(Convert.ToString(responserecipient.Result));

                // Now you can access properties of the recipient object
                recipientName = recipient1?.FirstName;
            }
            var response = await _serivce.GetAllAsync<APIResponse>(10, 1, token);
            if (response != null && response.IsSuccess)
            {
                hospitalList = JsonConvert.DeserializeObject<List<HospitalDTO>>(Convert.ToString(response.Result));
                this.HospitalOptions = new SelectList(this.hospitalList, "HospitalId", "Name");
            }
            DoseOptions = Enum.GetValues(typeof(Dose))
            .Cast<Dose>()
            .Select(dose => new SelectListItem
            {
                Value = dose.ToString(),
                Text = dose.ToString()
            })
            .ToList();
            var response1 = await _VaccineSerivce.GetAllAsync<APIResponse>(10, 1, token);
            if (response1 != null && response1.IsSuccess)
            {
                vaccineList = JsonConvert.DeserializeObject<List<VaccineScheduleDTO>>(Convert.ToString(response1.Result));
                UniqueScheduleDates = vaccineList
                    .Select(schedule => schedule.ScheduleDate) // Use Date property to extract only date part
                    .Distinct()
                    .ToList();
                UniqueTimeslots = vaccineList
                   .Select(schedule => schedule.TimeSlot) // Use Date property to extract only date part
                   .Distinct()
                   .ToList();

            }
        }
        public async Task OnGet(int id)
        {
            this.Token = HttpContext.Session.GetString(SessionData.Token);
            this.UserName = HttpContext.Session.GetString(SessionData.UserName);

            if ((string.IsNullOrEmpty(HttpContext.Session.GetInt32(SessionData.Discrimination).ToString()))
                || (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionData.UserName))))

                Redirect("/Logout");
            else
            {
                int? discriminationValue = HttpContext.Session.GetInt32(SessionData.Discrimination);

                if (discriminationValue.HasValue)
                {
                    this.VaccineRegistrationCreate.DependantRecipientId = discriminationValue.Value;
                    // Continue with your logic
                }
                TempData["Id"] = id;
                await this.LoadMasters(id);
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {

                if (ModelState.IsValid)
                {
                    string token = HttpContext.Session.GetString(SessionData.Token);
                    var response1 = await _VaccineSerivce.GetAllAsync<APIResponse>(10, 1, token);

                    if (response1 != null && response1.IsSuccess)
                    {
                        vaccineList = JsonConvert.DeserializeObject<List<VaccineScheduleDTO>>(Convert.ToString(response1.Result));
                        if (int.TryParse(VaccineRegistrationCreate.Hospital, out int hospitalId))
                        {
                            DateTime targetDate = VaccineRegistrationCreate.ScheduleDate.Date;

                            int Id = vaccineList.Where(schedule => schedule.HospitalId == hospitalId && schedule.ScheduleDate.Date == targetDate && schedule.TimeSlot == VaccineRegistrationCreate.TimeSlot).Select(schedule => schedule.VaccineScheduleId).FirstOrDefault();
                            VaccineRegistrationCreate.VaccineScheduleId = Id;
                        }
                    }

                    // Now you can access properties of the recipient object
                    VaccineRegistrationCreate.CreatedUser = HttpContext.Session.GetString(SessionData.UserName);

                    var response = await _vaccineRegistrationService.CreateAsync<APIResponseDTO>(VaccineRegistrationCreate, token);
                    if (response != null && response.IsSuccess)
                    {
                        //var userResult = JsonConvert.DeserializeObject<Model.DTO.RecipientRegistryDTO>(response.Result.ToString());
                        return Redirect("/User/DependantList");
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
                int exceptionId = 0; // Default value, you can set it to a meaningful default
                if (TempData.TryGetValue("Id", out object tempIdObj) && tempIdObj != null)
                {
                    if (int.TryParse(tempIdObj.ToString(), out int parsedId))
                    {
                        exceptionId = parsedId;
                        await this.LoadMasters(exceptionId);
                    }
                }

            }

            // If something went wrong, return to the current page with an error message
            int id = 0; // Default value, you can set it to a meaningful default
            if (TempData.TryGetValue("Id", out object tempId))
            {
                if (int.TryParse(tempId.ToString(), out int parsedId))
                {
                    id = parsedId;
                }
            }
            await this.LoadMasters(id);
            return Page();

        }
    }
}