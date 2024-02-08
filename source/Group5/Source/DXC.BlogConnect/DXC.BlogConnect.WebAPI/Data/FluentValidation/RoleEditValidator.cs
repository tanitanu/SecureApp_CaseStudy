using DXC.BlogConnect.DTO;
using FluentValidation;

namespace DXC.BlogConnect.WebAPI.Data.FluentValidation
{
    public class RoleEditValidator: AbstractValidator<RoleEditDTO>
    {
        public RoleEditValidator()
        {
            RuleFor(u => u.RoleName).NotEmpty()
                .WithErrorCode("rolename_required")
                .WithMessage("Role name cannot be empty");

        }

    }
}
