using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sus.Base.Core.Domain.User
{
    public class User_UserRole_Map:BaseEntity
    {
        public int UserId { get; set; }
        public int UserRoleId { get; set; }
        public virtual UserRole Role { get; set; }
        public virtual User User { get; set; }
    }
}
