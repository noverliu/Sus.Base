using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sus.Base.Core.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Sus.Base.Data.Mapping.User
{
    public class UserMapping : IEntityTypeConfiguration<Core.Domain.User.User>
    {
        public void Map(EntityTypeBuilder<Core.Domain.User.User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.RolesMap).WithOne(x => x.User).HasForeignKey(x=>x.UserId);
        }
    }
}
