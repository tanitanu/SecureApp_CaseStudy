using DXC.BlogConnect.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System;
using DXC.BlogConnect.WebAPI.Repositories.Interfaces;
using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.WebAPI.Models.DTO;

namespace DXC.BlogConnect.WebApp.Pages.Admin.Blogs
{
    public class AddModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;

        [BindProperty]
        public AddBlogPostDTO AddBlogPostRequest { get; set; }

        [BindProperty]
        public IFormFile FeaturedImage { get; set; }

        [BindProperty]
        [Required]
        public string Tags { get; set; }

        public AddModel(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            ValidateAddBlogPost();

            if (ModelState.IsValid)
            {
                var blogPost = new Post()
                {
                    Title = AddBlogPostRequest.Title,
                    Content = AddBlogPostRequest.Content,
                    ShortDescription = AddBlogPostRequest.ShortDescription,
                    ThumbnailUrl = AddBlogPostRequest.ThumbnailUrl,
                    PublishedDate = AddBlogPostRequest.PublishedDate,
                    Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag() { Name = x.Trim() }))
                };
                await blogPostRepository.AddAsync(blogPost);

                var notification = new Notification
                {
                    Type = DXC.BlogConnect.Utilities.Enums.NotificationType.Success,
                    Message = "New blog created!"
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage("/Admin/Blogs/List");
            }

            return Page();
        }

        private void ValidateAddBlogPost()
        {
            if (AddBlogPostRequest.PublishedDate.Date < DateTime.Now.Date)
            {
                ModelState.AddModelError("AddBlogPostRequest.PublishedDate",
                    $"PublishedDate can only be today's date or a future date.");
            }
        }
    }
}
