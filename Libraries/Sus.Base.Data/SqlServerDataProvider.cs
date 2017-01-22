using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Sus.Base.Core.Infrastructure.DependencyManagement;

namespace Sus.Base.Data
{
    public class SqlServerDataProvider: IDataProvider
    {
        public DbContextOptions<SusDbContext> BuildOptions(string connstr)
        {
            var optionBuilder = new DbContextOptionsBuilder<SusDbContext>();

            return optionBuilder.UseSqlServer(connstr, x =>
            {
                //x.CommandTimeout = 300;
            }).Options;
        }

        public void BuildOptions(IServiceCollection services, string constr)
        {
            services.AddEntityFrameworkSqlServer().AddDbContext<SusDbContext>(x =>
            {
                x.UseSqlServer(constr);
            });
        }

        public void SetDatabaseInitializer()
        {
            var dbcontext = StaticResolver.Resolve<SusDbContext>();
            if (!dbcontext.Database.EnsureCreated())
                dbcontext.Database.Migrate();
            //throw new NotImplementedException();
        }
    }
}
