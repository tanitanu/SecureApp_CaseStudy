using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.WebAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DXC.BlogConnect.WebApp.Pages.Blog
{
    public class DetailsModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IBlogPostCommentRepository blogPostCommentRepository;

        public Post BlogPost { get; set; }

        public List<Comments> Comments { get; set; }

        public int TotalLikes { get; set; }
        public bool Liked { get; set; }

        [BindProperty]
        public int BlogPostId { get; set; }

        [BindProperty]
        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public string CommentDescription { get; set; }


        public DetailsModel(IBlogPostRepository blogPostRepository,
            IBlogPostLikeRepository blogPostLikeRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IBlogPostCommentRepository blogPostCommentRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.blogPostCommentRepository = blogPostCommentRepository;
        }

        public async Task<IActionResult> OnGet(string urlHandle)
        {
            //await GetBlog(urlHandle);
            return Page();
        }

        public async Task<IActionResult> OnPost(string urlHandle)
        {
            if (ModelState.IsValid)
            {
                if (signInManager.IsSignedIn(User) && !string.IsNullOrWhiteSpace(CommentDescription))
                {
                    var userId = userManager.GetUserId(User);

                    var comment = new Comments()
                    {
                        Id = BlogPostId,
                        Comment = CommentDescription,
                         CreatedDate = DateTime.Now,
                         UpdatedDate = DateTime.Now
                        //UserId = Guid.Parse(userId)
                    };

                    await blogPostCommentRepository.AddAsync(comment);
                }

                return RedirectToPage("/Blog/Details", new { urlHandle = urlHandle });
            }

            //await GetBlog(urlHandle);
            return Page();
        }

        private async Task GetComments()
        {
            var blogPostComments = await blogPostCommentRepository.GetAllAsync(BlogPost.Id);

            var blogCommentsViewModel = new List<Comments>();
            foreach (var blogPostComment in blogPostComments)
            {
                blogCommentsViewModel.Add(new Comments
                {
                    CreatedDate = blogPostComment.CreatedDate,
                    Comment = blogPostComment.Comment
                    //Username = (await userManager.FindByIdAsync(blogPostComment.ComUserId.ToString())).UserName
                });
            }

            Comments = blogCommentsViewModel;
        }

        //private async Task GetBlog(string urlHandle)
        //{
        //    BlogPost = await blogPostRepository.GetAsync(urlHandle);

        //    if (BlogPost != null)
        //    {
        //        BlogPostId = BlogPost.Id;
        //        if (signInManager.IsSignedIn(User))
        //        {
        //            var likes = await blogPostLikeRepository.GetLikesForBlog(BlogPost.Id);

        //            var userId = userManager.GetUserId(User);

        //            Liked = likes.Any(x => x.UserId == Guid.Parse(userId));

        //            await GetComments();
        //        }

        //        TotalLikes = await blogPostLikeRepository.GetTotalLikesForBlog(BlogPost.Id);
        //    }
        //}
    }
}
