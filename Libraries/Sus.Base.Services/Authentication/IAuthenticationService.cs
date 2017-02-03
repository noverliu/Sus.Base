using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sus.Base.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<Core.Domain.User.User> GetAuthenticatedUser(HttpContext context);
        Task SignIn(HttpContext context,Core.Domain.User.User user, bool remember = false);
        Task SignOut(HttpContext context);
    }
}
