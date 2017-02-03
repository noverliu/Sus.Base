using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sus.Base.Core.Domain.User;

namespace Sus.Base.Services.User
{
    public interface IUserIdentifyService
    {
        LoginResult ValidateUser(Core.Domain.User.User user, string password);
    }
}
