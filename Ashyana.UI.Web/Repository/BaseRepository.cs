using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Ashyana.UI.Web.Models;

namespace Ashyana.UI.Web.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        public AshyanaDBEntities _context = null;
        private DbSet<T> table = null;
        public BaseRepository()
        {
            this._context = new AshyanaDBEntities();
            table = _context.Set<T>();
        }
        //example of using iqueryable to filter records
        public IQueryable<User> GetAllDataByCompanyName(string username)
        {
            return from u in _context.Users
                   where u.userName == username
                   orderby u.userEmailID descending
                   select u;
        }


        public IEnumerable<T> GetModel()
        {
            return table.ToList();
        }
        public T GetModelByID(int id)
        {
            return table.Find(id);
        }
        public void InsertModel(T obj)
        {
            table.Add(obj);
        }
        public void UpdateModel(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void DeleteModel(int id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
    }
}
//        public BaseRepository(DbContext context)
//        {
//            this.context = context;
//            this.dbSet = context.Set<T>();
//        }

//        public virtual List<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
//        {
//            IQueryable<T> query = dbSet;

//            foreach (Expression<Func<T, object>> include in includes)
//                query = query.Include(include);

//            if (filter != null)
//                query = query.Where(filter);

//            if (orderBy != null)
//                query = orderBy(query);

//            return query.ToList();
//        }

//        public virtual IQueryable<T> Query(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
//        {
//            IQueryable<T> query = dbSet;

//            if (filter != null)
//                query = query.Where(filter);

//            if (orderBy != null)
//                query = orderBy(query);

//            return query;
//        }

//        public virtual T GetById(object id)
//        {
//            return dbSet.Find(id);
//        }

//        public virtual T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
//        {
//            IQueryable<T> query = dbSet;

//            foreach (Expression<Func<T, object>> include in includes)
//                query = query.Include(include);

//            return query.FirstOrDefault(filter);
//        }

//        public virtual void Insert(T entity)
//        {
//            dbSet.Add(entity);
//        }

//        public virtual void Update(T entity)
//        {
//            dbSet.Attach(entity);
//            context.Entry(entity).State = EntityState.Modified;
//        }

//        public virtual void Delete(object id)
//        {
//            T entityToDelete = dbSet.Find(id);
//            if (context.Entry(entityToDelete).State == EntityState.Detached)
//            {
//                dbSet.Attach(entityToDelete);
//            }
//            dbSet.Remove(entityToDelete);
//        }
//    }

//}