using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sus.Base.Core.Domain.User;
using Sus.Base.Services.Security;

namespace Sus.Base.Services.User
{
    public class UserIdentifyService : IUserIdentifyService
    {
        private IEncryptionService _encryptionService;
        public UserIdentifyService(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        public LoginResult ValidateUser(Core.Domain.User.User user, string password)
        {
            if (user == null)
                return LoginResult.UserNotExists;
            string pwd;
            switch (user.PasswordFormat)
            {
                case PasswordFormat.Encrypted:
                    pwd = _encryptionService.EncryptText(password);
                    break;
                case PasswordFormat.Hashed:
                    pwd = _encryptionService.CreatePasswordHash(password, user.PasswordSalt);
                    break;
                default:
                    pwd = password;
                    break;
            }
            if (user.Password != pwd)
                return LoginResult.WrongPassword;
            return LoginResult.Successful;
        }
    }
}
