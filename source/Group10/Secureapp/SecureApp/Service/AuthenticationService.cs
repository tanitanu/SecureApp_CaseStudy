using SecureApp.SecureAppModel;
using System.Text.Json;
using System.Text;
using SecureApp.Model.SecureAppModel;

namespace SecureApp.Service
{
    public class AuthenticationService : IAuthenticateService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public AuthenticationService(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var registrationResult = await _client.PostAsJsonAsync("api/UserAccount/Registration", userForRegistration);
            var registrationContent = await registrationResult.Content.ReadAsStringAsync();
            if (!registrationResult.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<RegistrationResponseDto>(registrationContent, _options);
                return result;
            }
            return new RegistrationResponseDto { IsSuccessfulRegistration = true };
        }

        public async Task<LoginResponseDto> LoginUser(UserLoginDto userLoginDto)
        {
            var loginResult = await _client.PostAsJsonAsync("api/UserAccount/Login", userLoginDto);
            var loginContent = await loginResult.Content.ReadAsStringAsync();
            if (!loginResult.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<LoginResponseDto>(loginContent, _options);
                return result;
            }
            return new LoginResponseDto { IsSuccessfulLogin = true };
        }
    }
}
