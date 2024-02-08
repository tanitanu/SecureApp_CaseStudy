using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DXC.BlogConnect.DTO;
using DXC.BlogConnect.WebApp.Services.Interfaces;
using static DXC.BlogConnect.DTO.ErrorCode;
using DXC.BlogConnect.WebApp.ServiceExtension;

namespace DXC.BlogConnect.WebApp.Pages.Users
{
    public class EditModel : PageModel
    {

        private readonly IUserService _userService;
        public IList<UserEditDTO> UserList;
        public readonly IAuthService _authService;
        private readonly ILogger<EditModel> _logger;
        public List<Error> ErrorList;
        public EditModel(IUserService userService, IAuthService authService, ILogger<EditModel> logger)
        {
            _userService = userService;
            UserList = new List<UserEditDTO>();
            _authService = authService;
            _logger = logger;
            ErrorList = new List<Error>();
        }

        [BindProperty]
        public UserEditDTO UserEditDTO { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    RedirectToPage("/login");
                }
                var userId = User.Identity.GetUserId();
                var _userList = await _userService.GetUserByUserId(Convert.ToInt32(userId));
                if (_userList != null)
                {
                    var editDetails = _userList.Result.FirstOrDefault();
                    UserEditDTO = new UserEditDTO { Id = editDetails.Id, FirstName = editDetails.FirstName,LastName=editDetails.LastName, EmailId = editDetails.EmailId };
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                ErrorList.Add(new Error("ex", "Unhandled error"));
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }



            return RedirectToPage("./Index");
        }

    }
}
