using Multi.Tenanat.App.Factory;
using Multi.Tenant.Domain.Factory;
using Multi.Tenant.Domain.OLTP;
using Multi.Tenant.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Multi.Tenanat.Domain;

namespace Multi.Tenant.UnitOfWork.Classes
{
    /// <summary>
    /// Factory class to create UOW Object
    /// </summary>
    public class UnitOfWorkFactory : IUnitOfWorkFactory, IDisposable
    {
        private bool _disposed;
        IDataContextFactory dataContextFactory;
        IUnitOfWork unitOfWork;
        public UnitOfWorkFactory()
        {
            dataContextFactory = new DataContextFactory();
        }
        /// <summary>
        /// Create and Return Unit of work Object based on the enagegemet ID
        /// </summary>
        /// <param name="eng"></param>
        /// <returns></returns>
        public IUnitOfWork GetUnitOfWork(int eng)
        {
            OLTPContext oltpContext = dataContextFactory.GetDbContext(eng);
            unitOfWork = new UnitOfWork<OLTPContext>(oltpContext);
            return unitOfWork;
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this.unitOfWork.Dispose();
                }
            }
            this._disposed = true;
        }
    }
}
