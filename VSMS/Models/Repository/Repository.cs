using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace VSMS.Models.Repository
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private VSMS_Entities db;
        private DbSet<T> tbl;
        public Repository()
        {
            db = new VSMS_Entities();
            tbl = db.Set<T>();
        }
        public bool Add(T entity)
        {
            try
            {
                tbl.Add(entity);
                Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(T entity)
        {
            try
            {
                db.Entry(entity).State = EntityState.Modified;
                Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public T Get(object id)
        {
            return tbl.Find(id);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return tbl.Where(predicate).AsEnumerable();
        }

        public IEnumerable<T> GetAll()
        {
            return tbl.AsEnumerable();
        }

        public bool Remove(object id)
        {
            try
            {
                tbl.Remove(Get(id));
                Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Remove(T entity)
        {
            try
            {
                tbl.Remove(entity);
                Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public T SaveObject(T entity)
        {
            try
            {
                tbl.Add(entity);
                Save();
                db.Entry(entity).Reload();
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}