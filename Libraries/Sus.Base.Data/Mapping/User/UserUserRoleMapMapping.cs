using Sus.Base.Core.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sus.Base.Data.Mapping.User
{
    public class UserUserRoleMapMapping : EntityTypeConfiguration<User_UserRole_Map>
    {
        public override void Map(EntityTypeBuilder<User_UserRole_Map> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany(x => x.RolesMap).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Role).WithMany(x => x.UserMaps).HasForeignKey(x => x.UserRoleId);
        }
    }
}
