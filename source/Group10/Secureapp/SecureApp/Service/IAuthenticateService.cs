using SecureApp.Model.SecureAppModel;
using SecureApp.SecureAppModel;

namespace SecureApp.Service
{
    public interface IAuthenticateService
    {
        Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration);

        Task<LoginResponseDto> LoginUser(UserLoginDto loginDto);
    }
}
