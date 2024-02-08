using DXC.BlogConnect.DTO;
using DXC.BlogConnect.WebAPI.Models.DTO;
using FluentValidation;

namespace DXC.BlogConnect.WebAPI.Data.FluentValidation
{
    public class BlogPostValidator : AbstractValidator<BlogPostAddDTO>
    {
        public BlogPostValidator()
        {
            RuleFor(u => u.UserName).NotEmpty()
               .WithErrorCode("username_required")
               .WithMessage("User name cannot be empty");

            RuleFor(u => u.Title).NotEmpty()
               .WithErrorCode("title_required")
               .WithMessage("Title cannot be empty");

            RuleFor(u => u.Content).NotEmpty()
              .WithErrorCode("content_required")
              .WithMessage("Content cannot be empty");
        }
    }
}
