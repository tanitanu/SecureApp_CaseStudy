using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.DTO;
using FluentValidation;
namespace DXC.BlogConnect.WebAPI.Data.FluentValidation
{
    /*
* Created By: Kishore
*/
    public class UserValidator: AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(u => u.UserName).NotEmpty()
                .WithErrorCode("username_required")
                .WithMessage("User name cannot be empty");

            RuleFor(u => u.FirstName).NotEmpty()
                .WithErrorCode("firstname_required")
                .WithMessage("User name cannot be empty");

            RuleFor(u => u.EmailId).NotEmpty()
               .WithErrorCode("emailid_required")
               .WithMessage("EmailId cannot be empty");

            RuleFor(u => u.Password).NotEmpty()
               .WithErrorCode("firstname_required")
               .WithMessage("password cannot be empty");

            RuleFor(p => p.UserName).Length(6)
                .WithErrorCode("username_invalid")
                .WithMessage("User Name length must be 6");
        }
    }
}
