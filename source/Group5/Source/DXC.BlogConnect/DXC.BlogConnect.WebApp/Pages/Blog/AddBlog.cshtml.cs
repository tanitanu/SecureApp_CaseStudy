using DXC.BlogConnect.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System;
using static DXC.BlogConnect.DTO.ErrorCode;
using DXC.BlogConnect.WebApp.Services.Interfaces;
using DXC.BlogConnect.WebApp.Services;
using DXC.BlogConnect.WebAPI.Models.DTO;
using DXC.BlogConnect.WebApp.ServiceExtension;

namespace DXC.BlogConnect.WebApp.Pages.Blogs
{
    public class AddBlogModel : PageModel
    {

        [BindProperty]
        public BlogPostAddDTO AddBlogPostRequest { get; set; }

        [BindProperty]
        public IFormFile FeaturedImage { get; set; }

        [BindProperty]
        [Required]
        public string Tags { get; set; }

        private readonly IBlogService _blogService;
        private readonly ILogger<AddBlogModel> _logger;
        public List<Error> ErrorList;
        public AddBlogModel(IBlogService blogService, ILogger<AddBlogModel> logger)
        {
            _blogService=blogService;
            _logger = logger;
            ErrorList = new List<Error>();
        }
        public void OnGet()
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    RedirectToPage("/login");
                }
            }
        }

        public async Task<IActionResult> OnPost()
        {
            ValidateAddBlogPost();

            //if (ModelState.IsValid)
            //{
            //    //var blogPost = new Post()
                //{
                //    Title = AddBlogPostRequest.Title,
                //    Content = AddBlogPostRequest.Content,
                //    ShortDescription = AddBlogPostRequest.ShortDescription,
                //    ThumbnailUrl = AddBlogPostRequest.ThumbnailUrl,
                //    PublishedDate = AddBlogPostRequest.PublishedDate,
                //    Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag() { Name = x.Trim() }))
                //};
                var request = AddBlogPostRequest;
                if (User != null & User.Identity !=null)
                {
                    request.UserName = User.Identity.GetUserName();
                }
                var res = await _blogService.AddBlogAsync(request);

                if (res.ErrorMessages.Any())
                {
                    ErrorList = res.ErrorMessages.ToList();

                    return Page();
                }

                if (res.IsSuccess)
                    return RedirectToPage("./Index");
                else
                //return Page();
                {
                    var notification = new Notification
                    {
                        Type = Utilities.Enums.NotificationType.Success,
                        Message = "New blog created!"
                    };

                    TempData["Notification"] = JsonSerializer.Serialize(notification);
                }
                return RedirectToPage("/Admin/Blogs/List");
            //}

            return Page();
        }

        private void ValidateAddBlogPost()
        {
            if (AddBlogPostRequest.PublishedDate < DateTime.Now)
            {
                ModelState.AddModelError("AddBlogPostRequest.PublishedDate",
                    $"PublishedDate can only be today's date or a future date.");
            }
        }
    }
}
