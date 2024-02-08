using DXC.BlogConnect.WebAPI.Repositories.Interfaces;
using DXC.BlogConnect.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DXC.BlogConnect.WebAPI.Models.Domain;

namespace DXC.BlogConnect.WebApp.Pages.Tags
{
    public class DetailsModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;

        public List<Post> Blogs { get; set; }

        public DetailsModel(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        public async Task<IActionResult> OnGet(string tagName)
        {
            Blogs = (await blogPostRepository.GetAllAsync(tagName)).ToList();
            return Page();
        }
    }
}
