using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sus.Base.Core.Domain.User
{
    public class User:BaseEntity
    {
        private ICollection<User_UserRole_Map> _roles;
        public User()
        {
            this.Guid = new Guid();
        }
        public string UserName { get; set; }
        public Guid Guid { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<User_UserRole_Map> RolesMap
        {
            get { return _roles ?? (_roles = new List<User_UserRole_Map>()); }
            set { _roles = value; }
        }
    }
}
