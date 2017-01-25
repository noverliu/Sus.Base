using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sus.Base.Core.Domain.User
{
    public class UserRole:BaseEntity
    {
        private ICollection<User_UserRole_Map> _userMaps;
        public string RoleName { get; set; }
        public string Description { get; set; }
        public ICollection<User_UserRole_Map> UserMaps
        {
            get { return _userMaps ?? (_userMaps = new List<User_UserRole_Map>()); }
            set { _userMaps = value; }
        }
    }
}
