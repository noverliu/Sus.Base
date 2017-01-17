using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sus.Base.Data
{
    public interface IDataProvider
    {
        DbContextOptions<SusDbContext> BuildOptions(string connstr);
        void SetDatabaseInitializer();
    }
}
