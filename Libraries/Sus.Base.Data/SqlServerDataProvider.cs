using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public void SetDatabaseInitializer()
        {
            //throw new NotImplementedException();
        }
    }
}
