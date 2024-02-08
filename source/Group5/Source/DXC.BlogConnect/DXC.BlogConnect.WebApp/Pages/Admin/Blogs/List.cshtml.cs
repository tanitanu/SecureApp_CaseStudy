using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.WebAPI.Repositories.Interfaces;
using DXC.BlogConnect.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace DXC.BlogConnect.WebApp.Pages.Admin.Blogs
{
    public class ListModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;

        public List<Post> BlogPosts { get; set; }

        public ListModel(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        public async Task OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }

            BlogPosts = (await blogPostRepository.GetAllAsync())?.ToList();
        }
    }
}
