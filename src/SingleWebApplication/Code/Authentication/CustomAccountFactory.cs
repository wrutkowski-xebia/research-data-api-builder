using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;

namespace SingleWebApplication.Code.Authentication
{
    public class CustomAccountFactory : AccountClaimsPrincipalFactory<CustomUserAccount>
    {
        public CustomAccountFactory(NavigationManager navigation, IAccessTokenProviderAccessor accessor)
            : base(accessor)
        {
        }

        public override async ValueTask<ClaimsPrincipal> CreateUserAsync(
            CustomUserAccount account, RemoteAuthenticationUserOptions options)
        {
            var initialUser = await base.CreateUserAsync(account, options);

            if (initialUser.Identity != null && initialUser.Identity.IsAuthenticated)
            {
                var userIdentity = (ClaimsIdentity)initialUser.Identity;

                foreach (var role in account.Roles)
                {
                    userIdentity.AddClaim(new Claim(AuthConst.RoleClaim, role));
                }
            }

            return initialUser;
        }
    }
}
