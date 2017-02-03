using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sus.Base.Core.Domain.User
{
    public enum LoginResult
    {
        Successful=1,
        UserNotExists=2,
        WrongPassword=3
    }
}
