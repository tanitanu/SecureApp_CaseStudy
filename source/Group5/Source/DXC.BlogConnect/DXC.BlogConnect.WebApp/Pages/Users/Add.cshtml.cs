using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DXC.BlogConnect.DTO;
using DXC.BlogConnect.WebApp.Services.Interfaces;
using static DXC.BlogConnect.DTO.ErrorCode;

namespace DXC.BlogConnect.WebApp.Pages.Users
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class AddModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ILogger<AddModel> _logger;
        public List<Error> ErrorList;
        public AddModel(IUserService userService, ILogger<AddModel> logger)
        {
            _userService = userService;
            _logger = logger;
            ErrorList = new List<Error>();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UserDTO UserAddDTO { get; set; } = default!;
        
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid || UserAddDTO == null)
                {
                    return Page();
                }

                var res = await _userService.AddUserAsync(UserAddDTO);
                
                if (res.ErrorMessages.Any())
                {
                   ErrorList= res.ErrorMessages.ToList();

                    return Page();
                }

                if (res.IsSuccess)
                    return RedirectToPage("./Index");
                else
                    return Page();
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
