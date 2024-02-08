using BlazorCARS.HealthShield.Utility;
using BlazorCARS.HealthShield.WebApp.Model.DTO;
using BlazorCARS.HealthShield.WebApp.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlazorCARS.HealthShield.WebApp.Pages
{
    public class LoginModel : PageModel
    {

        //public void OnGet()
        //{
        //}
        private readonly IAuthService _serivce;
        public string ErrorMessage { get; set; }
        //public List<APIResponseDTO> apiResult { get; set; }
        public UserDTO loginModel { get; set; }
        public LoginModel(IAuthService authSerivce)
        {
            _serivce = authSerivce;
        }

        public async Task<IActionResult> OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(UserDTO loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _serivce.LoginAsync<APIResponseDTO>(loginModel);

                    if (response != null && response.IsSuccess)
                    {

                        if (response.Result is JObject resultObject) // Assuming Newtonsoft.Json
                        {
                            // Convert the Result property to a specific type if needed
                            var userResult = JsonConvert.DeserializeObject<LoginResponseDTO>(resultObject.ToString());
                            HttpContext.Session.SetString(SessionData.UserName, userResult.UserName);
                            HttpContext.Session.SetString(SessionData.UserRole, userResult.UserRole);
                            HttpContext.Session.SetInt32(SessionData.Discrimination, userResult.DiscriminationId ?? 0);
                            HttpContext.Session.SetString(SessionData.Token, userResult.Token);

                            //if (userResult.UserRole == DataObject.Enums.UserRole.User.ToString())
                            //{
                            //    return Redirect("User/DependantList");
                            //}
                            //else if (userResult.UserRole == DataObject.Enums.UserRole.Admin.ToString()) //Hospital
                            //{
                            //    return Redirect("Hospital/ViewHospital");
                            //}
                            //else//Super Admin
                            //{
                            //    return Redirect("User/List");
                            //}
                            return Redirect("/Index");
                        }
                    }
                    else
                    {
                        //string data = response.Content.ReadAsStringAsync().Result;
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
                    ErrorMessage = "Invalid model state.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Exception: {ex.Message}";
            }

            // If something went wrong, return to the current page with an error message
            return Page();
        }
    }
}
