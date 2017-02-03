using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sus.Base.Core.Domain.User;
using Sus.Base.Core;

namespace Sus.Base.Services.User
{
    public class UserService : IUserService
    {
        private IRepository<Core.Domain.User.User> _userRepository;
        public UserService(IRepository<Core.Domain.User.User> userRepository)
        {
            _userRepository = userRepository;
        }
        public int DeleteUser(Core.Domain.User.User user)
        {
            return _userRepository.Delete(user);
        }

        public Core.Domain.User.User GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }

        public int InsertUser(Core.Domain.User.User user)
        {
            return _userRepository.Insert(user);
        }

        public int UpdatUser(Core.Domain.User.User user)
        {
            return _userRepository.Insert(user);
        }

        public Core.Domain.User.User GetUserByName(string username)
        {
            return _userRepository.Table.SingleOrDefault(x => x.UserName == username);
        }
    }
}
