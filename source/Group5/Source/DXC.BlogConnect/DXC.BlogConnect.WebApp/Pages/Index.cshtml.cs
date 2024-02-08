using DXC.BlogConnect.DTO;
using DXC.BlogConnect.WebApp.Pages.Users;
using DXC.BlogConnect.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static DXC.BlogConnect.DTO.ErrorCode;

namespace DXC.BlogConnect.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IBlogService _blogService;
        public IList<BlogPostGetDTO> BlogList;
        private readonly ILogger<IndexModel> _logger;
        public List<Error> ErrorList;
        public IndexModel(IBlogService blogService, ILogger<IndexModel> logger)
        {
            _blogService = blogService;
            BlogList = new List<BlogPostGetDTO>();
            ErrorList = new List<Error>();
            _logger = logger;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var _userList = await _blogService.GetAllBlogsAsync();

            if (_userList != null)
            {
                BlogList = _userList.Result;
            }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                ErrorList.Add(new Error("ex", "Unhandled error"));
            }
            return Page();
        }

    }
}