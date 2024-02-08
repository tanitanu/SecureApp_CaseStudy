using BlazorCARS.HealthShield.Utility;
using BlazorCARS.HealthShield.WebApp.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BlazorCARS.HealthShield.WebApp.Pages.User
{
    [BindProperties]
    public class EditDependantModel : PageModel
    {
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
        private readonly IRecipientService _serivce;
        public Model.DTO.RecipientUpdateDTO Recipient { get; set; }

        public EditDependantModel(IRecipientService recipientService)
        {
            this._serivce = recipientService;
            this.Recipient = new Model.DTO.RecipientUpdateDTO();
        }

        public async Task<IActionResult> OnGet(int id)
        {
            if ((string.IsNullOrEmpty(HttpContext.Session.GetInt32(SessionData.Discrimination).ToString()))
                || (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionData.UserName)))
                || (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionData.Token))))
            {
                Redirect("/Logout");
            }
            else
            {
                this.Token = HttpContext.Session.GetString(SessionData.Token);
                this.Recipient.UpdatedUser = HttpContext.Session.GetString(SessionData.UserName);
                this.Recipient.DependentRecipientId = int.Parse(HttpContext.Session.GetInt32(SessionData.Discrimination).ToString());

                var response = await _serivce.GetAsync<Model.DTO.APIResponseDTO>(id, this.Token);
                if (response != null && response.IsSuccess)
                {
                    this.Recipient = JsonConvert.DeserializeObject<Model.DTO.RecipientUpdateDTO>(response.Result.ToString());
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
                    this.Recipient.UpdatedUser = HttpContext.Session.GetString(SessionData.UserName);

                    var response = await _serivce.UpdateAsync<Model.DTO.APIResponseDTO>(this.Recipient.RecipientId, this.Recipient, this.Token);
                    if (response != null && response.IsSuccess)
                    {
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
            }

            return Page();
        }
    }
}
