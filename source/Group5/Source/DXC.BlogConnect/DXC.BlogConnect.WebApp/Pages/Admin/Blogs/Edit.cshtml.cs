using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.WebAPI.Repositories.Interfaces;
using DXC.BlogConnect.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System;
using DXC.BlogConnect.DTO;
using System.Text.Json;

namespace DXC.BlogConnect.WebApp.Pages.Admin.Blogs
{
    public class EditModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;

        [BindProperty]
        public EditBlogPostRequest BlogPost { get; set; }

        [BindProperty]
        public IFormFile FeaturedImage { get; set; }

        [BindProperty]
        [Required]
        public string Tags { get; set; }

        public EditModel(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        public async Task OnGet(int id)
        {
            var blogPostDomainModel = await blogPostRepository.GetAsync(id);

            if (blogPostDomainModel != null && blogPostDomainModel.Tags != null)
            {
                BlogPost = new EditBlogPostRequest
                {
                    Id = blogPostDomainModel.Id,
                    Title = blogPostDomainModel.Title,
                    Content = blogPostDomainModel.Content,
                    ShortDescription = blogPostDomainModel.ShortDescription,
                    ThumbnailUrl = blogPostDomainModel.ThumbnailUrl,
                    PublishedDate = blogPostDomainModel.PublishedDate
                };

                Tags = string.Join(',', blogPostDomainModel.Tags.Select(x => x.Name));
            }
        }

        public async Task<IActionResult> OnPostEdit()
        {
            ValidateEditBlogPost();

            if (ModelState.IsValid)
            {
                try
                {
                    var blogPostDomainModel = new Post
                    {
                        Id = BlogPost.Id,
                        Title = BlogPost.Title,
                        Content = BlogPost.Content,
                        ShortDescription = BlogPost.ShortDescription,
                        ThumbnailUrl = BlogPost.ThumbnailUrl,
                        PublishedDate = BlogPost.PublishedDate,
                        Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag() { Name = x.Trim() }))
                    };


                    await blogPostRepository.UpdateAsync(blogPostDomainModel);

                    ViewData["Notification"] = new Notification
                    {
                        Type = DXC.BlogConnect.Utilities.Enums.NotificationType.Success,
                        Message = "Record updated successfully!"
                    };
                }
                catch (Exception ex)
                {
                    ViewData["Notification"] = new Notification
                    {
                        Type = Utilities.Enums.NotificationType.Error,
                        Message = "Something went wrong!"
                    };
                }

                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await blogPostRepository.DeleteAsync(BlogPost.Id);
            if (deleted)
            {
                var notification = new Notification
                {
                    Type = Utilities.Enums.NotificationType.Success,
                    Message = "Blog was deleted successfully!"
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage("/Admin/Blogs/List");
            }

            return Page();
        }


        private void ValidateEditBlogPost()
        {
            if (!string.IsNullOrWhiteSpace(BlogPost.Title))
            {
                // check for minimum length
                if (BlogPost.Title.Length < 10 || BlogPost.Title.Length > 72)
                {
                    ModelState.AddModelError("BlogPost.Title",
                        "Heading can only be between 10 and 72 characters.");
                }
                // check for maximum length
            }
        }
    }
}
