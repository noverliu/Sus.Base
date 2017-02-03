using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sus.Base.Core.Domain.User;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;
using Sus.Base.Services.User;

namespace Sus.Base.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private IUserService _userService;
        public AuthenticationService(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<Core.Domain.User.User> GetAuthenticatedUser(HttpContext context)
        {
            var a = await context.Authentication.GetAuthenticateInfoAsync("Cookie");
            var user = _userService.GetUserByName(context.User.Identity.Name);
            return user;
        }

        public async Task SignIn(HttpContext context, Core.Domain.User.User user,bool remember=false)
        {
            //you can add all of ClaimTypes in this collection 
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName) 
            };

            //init the identity instances 
            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "SuperSecureLogin"));

            //signin 
            await context.Authentication.SignInAsync("Cookie", userPrincipal, new AuthenticationProperties
            {
                ExpiresUtc = remember?DateTime.UtcNow.AddDays(30):DateTime.UtcNow.AddMinutes(20),
                IsPersistent = false,
                AllowRefresh = false
            });
        }

        public async Task SignOut(HttpContext context)
        {
            await context.Authentication.SignOutAsync("Cookie");
        }
    }
}
