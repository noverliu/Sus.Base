using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Sus.Base.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sus.Base.Data
{
    public class SusDbContext : DbContext, IDbContext
    {
        private DbContextOptions<SusDbContext> _option;
        public SusDbContext() 
        {

        }
        public SusDbContext(DbContextOptions<SusDbContext> options) : base(options)
        {
            _option = options;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister = typeof(EntityTypeConfiguration<>).GetTypeInfo().Assembly.GetTypes()
            .Where(type => !String.IsNullOrEmpty(type.Namespace))
            .Where(type => type.GetTypeInfo().BaseType != null && type.GetTypeInfo().BaseType.GetTypeInfo().IsGenericType &&
                type.GetTypeInfo().BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.AddConfiguration((EntityTypeConfiguration<dynamic>)configurationInstance);
            }
            base.OnModelCreating(modelBuilder);   
        }
        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            try
            {
                //logger.Debug("EntitySet" + typeof(TEntity).ToString());
                return base.Set<TEntity>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("类型错误----" + typeof(TEntity).ToString() + ";" + ex.Message);
                throw;
            }

        }
        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = default(int?), params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public IDbContextTransaction BeginTrans()
        {
           return base.Database.BeginTransaction();
        }
    }
}
