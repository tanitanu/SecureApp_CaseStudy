using DXC.BlogConnect.DTO;
using FluentValidation;

namespace DXC.BlogConnect.WebAPI.Data.FluentValidation
{
    public class UserLoginValidator:AbstractValidator<UserLoginDTO>
    {
        public UserLoginValidator()
        {
            RuleFor(u => u.UserName).NotEmpty()
                .WithErrorCode("username_required")
                .WithMessage("User name cannot be empty");

            RuleFor(u => u.Password).NotEmpty()
                .WithErrorCode("password_required")
                .WithMessage("password cannot be empty");
        }

        }
}
