using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.WebApp.Models;

namespace TaskManagement.WebApp.Pages.TaskMgmt
{
    #region [Summary]
    ///<author>Ranjna Devi</author>
    ///<date>20-Nov-2023</date>
    ///<project>TaskManagement.WebApp</project>
    ///<class>Delete Model</class>
    /// <summary>
    /// This is the page model class for task details 
    /// </summary>
    #endregion [Summary]
    public class DeleteModel : PageModel
    {
        #region [Private Variables]
        private readonly HttpClient _httpClient;
        #endregion [Private Variables]

        #region [Public Variables]
        /// <summary>
        /// Variable to hold task details
        /// </summary>
        [BindProperty]
        public Tasks Task { get; set; }
        #endregion [Public Variables]

        #region [Constructor]
        public DeleteModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        #endregion [Constructor]

        #region [public methods]
        /// <summary>
        /// OnGetAsync
        /// </summary>
        /// <param name="id">Task Id</param>
        /// <returns>Details of task based on task id by consuming web api</returns>
        public async Task OnGetAsync(Guid id)
        {
            var apiUrl = "https://localhost:7144/api/tasks";
            Task = await _httpClient.GetFromJsonAsync<Tasks>(apiUrl + "/{" + id.ToString() + "}");
        }
        public async Task<IActionResult> OnPost(Guid id)
        {
            var apiUrl = "https://localhost:7144/api/tasks";
            if (id != null)
            {
                var response = await _httpClient.DeleteAsync(apiUrl + "/{" + id.ToString() + "}");
                TempData["success"] = "Task deleted successfully";
                return RedirectToPage("/TaskMgmt/ViewList");
            }
            return Page();
        }
        #endregion [public methods]
    }
}

