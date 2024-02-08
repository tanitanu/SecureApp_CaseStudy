using BlazorCARS.HealthShield.Utility;
using BlazorCARS.HealthShield.WebApp.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorCARS.HealthShield.WebApp.Pages.User
{
    public class AddDependantModel : PageModel
    {
        public string Token { get; set; }
        public string UserName { get; set; }

        private readonly IRecipientService _serivce;
        public string ErrorMessage { get; set; }

        [BindProperty]
        public Model.DTO.RecipientCreateDTO Recipient { get; set; }

        public AddDependantModel(IRecipientService recipientService)
        {
            this._serivce = recipientService;
            this.Recipient= new Model.DTO.RecipientCreateDTO();
        }

        public void OnGet()
        {
            this.Token = HttpContext.Session.GetString(SessionData.Token);
            this.UserName = HttpContext.Session.GetString(SessionData.UserName);

            if ((string.IsNullOrEmpty(HttpContext.Session.GetInt32(SessionData.Discrimination).ToString()))
                || (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionData.UserName))))

                Redirect("/Logout");
            else
            {
                this.Recipient.DependentRecipientId = int.Parse(HttpContext.Session.GetInt32(SessionData.Discrimination).ToString());
                this.Recipient.CreatedUser = HttpContext.Session.GetString(SessionData.UserName);
            }

            this.Recipient.DOB = DateTime.Now;

        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.Token = HttpContext.Session.GetString(SessionData.Token);
                    var response = await _serivce.CreateAsync<Model.DTO.APIResponseDTO>(this.Recipient, this.Token);

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

            // If something went wrong, return to the current page with an error message
            return Page();
        }
    }
}
