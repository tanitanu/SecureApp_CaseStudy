using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace MeetingScheduler.Authentication
{
    public class CustomAuthenticationStateprovider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _principal = new ClaimsPrincipal(new ClaimsIdentity());

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                //var claims = new[] { new Claim(ClaimTypes.Name, "rahul.kumar5@dxc.com") };
                //identity = new ClaimsIdentity(claims, "apiauth_type");
                //identity=new ClaimsIdentity();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Request failed:" + ex.ToString());
            }
            return Task.FromResult (new AuthenticationState(new ClaimsPrincipal(identity)));
        }

        public void MarkUserAsAuthenticated(string emailAddress, string Role)
        {
            ClaimsPrincipal claimsPrincipal;
            var identity = new ClaimsIdentity();
            if (emailAddress != null && Role != null)
            {

                try
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, emailAddress), new Claim(ClaimTypes.Role, Role) };
                    identity = new ClaimsIdentity(claims, "apiauth_type");
                    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine("Request failed:" + ex.ToString());
                }

            }
            else
            {

                claimsPrincipal = _principal;
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
            }
        }
    }
}
