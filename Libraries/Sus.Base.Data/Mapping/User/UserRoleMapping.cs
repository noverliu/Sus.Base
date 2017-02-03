using Sus.Base.Core.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Sus.Base.Data.Mapping.User
{
    public class UserRoleMapping : EntityTypeConfiguration<UserRole>
    {
        public override void Map(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.UserMaps).WithOne().HasForeignKey(x => x.UserRoleId);
        }
    }
}
