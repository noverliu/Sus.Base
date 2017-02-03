using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Sus.Base.Core.Infrastructure.DependencyManagement;
using Sus.Base.Core.Domain.User;
using Sus.Base.Core;

namespace Sus.Base.Data
{
    public class SqlServerDataProvider: IDataProvider
    {
        private IRepository<User> _userRepo;
        private IRepository<User_UserRole_Map> _userUserRoleMapRepo;
        private IRepository<UserRole> _userRoleRepo;

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
            var dbcontext = StaticResolver.Resolve<IDbContext>();
            if (dbcontext.db().EnsureCreated())
                BuildSeed(dbcontext);
            else
                dbcontext.db().Migrate();
        }
        private void BuildSeed(IDbContext _context)
        {
            //_context.BeginTrans();
            var admin = new User
            {
                UserName = "Admin",
                Password = "Aa111111",
                DisplayName = "管理员"
            };
            var administrators = new UserRole
            {
                RoleName = "管理员组"
            };
            _userRepo = StaticResolver.Resolve<IRepository<User>>();
            _userRoleRepo = StaticResolver.Resolve<IRepository<UserRole>>();
            _userUserRoleMapRepo = StaticResolver.Resolve<IRepository<User_UserRole_Map>>();
            _context.BeginTrans();
            _userRepo.Insert(admin);
            _userRoleRepo.Insert(administrators);
            _context.SaveChanges();
            var relation=new User_UserRole_Map
            {
                UserId=admin.Id,
                UserRoleId = administrators.Id
            };
            _userUserRoleMapRepo.Insert(relation);
            //_context.SaveChanges();
        }
    }
}
