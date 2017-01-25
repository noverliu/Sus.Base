using Sus.Base.Core.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sus.Base.Data.Mapping.User
{
    public class UserUserRoleMapMapping : IEntityTypeConfiguration<User_UserRole_Map>
    {
        public void Map(EntityTypeBuilder<User_UserRole_Map> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
