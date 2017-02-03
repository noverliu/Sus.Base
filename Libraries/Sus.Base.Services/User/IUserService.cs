using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sus.Base.Core.Domain.User;

namespace Sus.Base.Services.User
{
    public interface IUserService
    {
        Core.Domain.User.User GetUserByName(string username);
        Core.Domain.User.User GetUserById(int id);
        int UpdatUser(Core.Domain.User.User user);
        int DeleteUser(Core.Domain.User.User user);
        int InsertUser(Core.Domain.User.User user);
    }
}
