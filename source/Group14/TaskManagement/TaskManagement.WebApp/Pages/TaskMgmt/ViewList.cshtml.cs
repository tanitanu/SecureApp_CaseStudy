#region [Using]
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using TaskManagement.WebApp.Models;
#endregion [Using]

namespace TaskManagement.WebApp.Pages.TaskMgmt
{
    #region [Summary]
    ///<author>Poornima Shanbhag</author>
    ///<date>17-Nov-2023</date>
    ///<project>TaskManagement.WebApp</project>
    ///<class>ViewListModel</class>
    /// <summary>
    /// This is the page model class for Task List View 
    /// </summary>
    #endregion [Summary]
    public class ViewListModel : PageModel
    {
        #region [Private Variables]
        private readonly HttpClient _httpClient;
        #endregion [Private Variables]

        #region [Public Variables]
        /// <summary>
        /// Variable to hold the task details
        /// </summary>
        public List<Tasks> TaskList = new List<Tasks>();
        #endregion [Public Variables]

        #region [Constructor]
        public ViewListModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        #endregion [Constructor]

        #region [public methods]
        /// <summary>
        /// OnGetAsync
        /// </summary>
        /// <returns>Load the page with task list consuming web api</returns>
        public async Task OnGetAsync()
        {
            var apiUrl = "https://localhost:7144/api/tasks";

            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                TaskList = JsonConvert.DeserializeObject<List<Tasks>>(content);

                // Use the 'values' data as needed
            }
            else
            {
                // Handle error
            }
        }
        #endregion [public methods]
    }
}
