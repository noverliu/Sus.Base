using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Sus.Base.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sus.Base.Data
{
    public class SusRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IDbContext _context;
        private DbSet<T> _entities;
        private IDbContextTransaction _trans
        {
            get
            {
                if (_context == null)
                    return null;
                return _context.db().CurrentTransaction;
            }
        }

        public SusRepository(IDbContext context)
        {
            _context = context;
        }
        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }
        public IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        public IQueryable<T> TableNoTracking
        {
            get
            {
                return this.Entities.AsNoTracking();
            }
        }

        public void Commit()
        {
            
            if (_trans != null)
            {
                try
                {
                    _trans.Commit();
                }
                catch 
                {
                    _trans.Rollback();
                    throw;
                }
                finally
                {
                    _trans.Dispose();
                }
            }
                
        }

        public int Delete(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities)
                    this.Entities.Remove(entity);
                if (_trans == null)
                    return this._context.SaveChanges();
                return 0;
            }
            catch 
            {
                throw;
            }
        }

        public int Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Remove(entity);
                if (_trans == null)
                    return this._context.SaveChanges();
                return 0;
            }
            catch 
            {
                throw;
            }
        }

        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public int Insert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities)
                    this.Entities.Add(entity);
                if (_trans == null)
                    return this._context.SaveChanges();
                return 0;
            }
            catch 
            {
                throw;
            }
        }

        public int Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Add(entity);
                if (_trans == null)
                    return this._context.SaveChanges();
                return 0;
            }
            catch 
            {
                throw;
            }
        }

        public void RollBack()
        {
            if (_trans != null)
            {
                _trans.Rollback();
                _trans.Dispose();
            }
                
        }

        public int Update(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entity");
                if (_trans == null)
                    return this._context.SaveChanges();
                return 0;
            }
            catch 
            {
                throw;
            }
        }

        public int Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                if (_trans == null)
                    return this._context.SaveChanges();
                return 0;
            }
            catch 
            {
                throw;
            }
        }

        public void BeginTrans()
        {
            if (_trans != null)
                return;
            _context.BeginTrans();
        }
    }
}
