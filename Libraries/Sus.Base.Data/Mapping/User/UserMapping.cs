using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sus.Base.Core.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Sus.Base.Data.Mapping.User
{
    public class UserMapping : EntityTypeConfiguration<Core.Domain.User.User>
    {
        public override void Map(EntityTypeBuilder<Core.Domain.User.User> builder)
        {
            builder.ToTable("User");
            builder.Ignore(x => x.PasswordFormat);
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.RolesMap).WithOne(x => x.User).HasForeignKey(x=>x.UserId);
        }
    }
}
