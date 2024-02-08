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
    ///<author>Poornima Shanbhag</author>
    ///<date>17-Nov-2023</date>
    ///<project>TaskManagement.WebApp</project>
    ///<class>CreateModel</class>
    /// <summary>
    /// This is the page model class for create task 
    /// </summary>
    #endregion [Summary]
    [BindProperties]
    public class CreateModel : PageModel
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
        public CreateModel(HttpClient httpClient)
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
            var apiUrl = "https://localhost:7144/api/Tasks/api/Tasks/Status";

            if (id == default)
            {
                var response = await _httpClient.GetAsync(apiUrl);

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
        }
        /// <summary>
        /// OnPost
        /// </summary>
        /// <returns>Saves the task to database by consuming web api</returns>
        public async Task<IActionResult> OnPost()
        {
            var apiUrl = "https://localhost:7144/api/tasks";

            if (ModelState.IsValid)
            {
                await _httpClient.PostAsJsonAsync(apiUrl, Tasks);
                TempData["success"] = "Task created successfully";
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
