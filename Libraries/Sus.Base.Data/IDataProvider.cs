using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sus.Base.Data
{
    public interface IDataProvider
    {
        DbContextOptions<SusDbContext> BuildOptions(string connstr);
        void BuildOptions(IServiceCollection services, string constr);
        void SetDatabaseInitializer();
    }
}
