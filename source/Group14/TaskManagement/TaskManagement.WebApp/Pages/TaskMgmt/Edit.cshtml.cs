#region [Using]
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using TaskManagement.WebApp.Models;
#endregion [Using]
namespace TaskManagement.WebApp.Pages.TaskMgmt
{
    #region [Summary]
    ///<author>Ranjna Devi</author>
    ///<date>20-Nov-2023</date>
    ///<project>TaskManagement.WebApp</project>
    ///<class>EditModel</class>
    /// <summary>
    /// This is the page model class for Edit task 
    /// </summary>
    #endregion [Summary]
    [BindProperties]
    public class EditModel : PageModel
    {
        #region [Private Variables]
        private readonly HttpClient _httpClient;
        static IEnumerable<SelectListItem> StatusData;
        #endregion [Private Variables]
        #region [Public Variables]
        /// <summary>
        /// Varibale to bind the status list to status drop down
        /// </summary>
        public IEnumerable<SelectListItem> StatusList { get; set; }
        /// <summary>
        /// Variable to hold Task list
        /// </summary>
        public Tasks Tasks { get; set; }
        #endregion [Public Variables]

        #region [Constructor]
        public EditModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        #endregion [Constructor]

        #region [public methods]
        /// <summary>
        /// OnGetAsync
        /// </summary>
        /// <param name="id">Fetch status list by consuming web api</param>
        public async Task OnGetAsync(Guid id)
        {
            var taskApiUrl = "https://localhost:7144/api/tasks";

            Tasks = await _httpClient.GetFromJsonAsync<Tasks>(taskApiUrl + "/{" + id.ToString() + "}");

            var statusApiUrl = "https://localhost:7144/api/Tasks/api/Tasks/Status";
            var response = await _httpClient.GetAsync(statusApiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                List<Status> status = JsonConvert.DeserializeObject<List<Status>>(content);

                StatusList = status.Select(i => new SelectListItem()
                {
                    Text = i.Code,
                    Value = i.Id.ToString()
                });

                StatusData = StatusList;
                // Use the 'values' data as needed
            }
            else
            {
                // Handle error
            }
        }
        /// <summary>
        /// OnPut
        /// </summary>
        /// <returns>Saves the task to database by consuming web api</returns>
        public async Task<IActionResult> OnPost(Guid id)
        {
            var apiUrl = "https://localhost:7144/api/tasks";

            if (ModelState.IsValid)
            {
                var response= await _httpClient.PutAsJsonAsync(apiUrl + "/{" + id.ToString() + "}", Tasks);
                TempData["success"] = "Task updated successfully";
                return RedirectToPage("/TaskMgmt/ViewList");
            }
            else
            {
                StatusList = StatusData;
            }
            return Page();
        }
        #endregion [public methods]
    }
}

