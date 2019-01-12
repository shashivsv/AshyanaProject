using Ashyana.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ashyana.UI.Web.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
    
        private readonly AshyanaDBEntities _context;
        private IBaseRepository<Model> _modelRepository;

        public UnitOfWork(AshyanaDBEntities context)
        {
            _context = context;
        }

        public IBaseRepository<Model> ModelRepository
        {
            get { return _modelRepository ?? (_modelRepository = new BaseRepository<Model>(_context)); }
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

  