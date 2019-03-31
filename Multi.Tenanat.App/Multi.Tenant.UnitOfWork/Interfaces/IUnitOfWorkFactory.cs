using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi.Tenant.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkFactory : IDisposable
    {
        IUnitOfWork GetUnitOfWork(int eng);
    }
}
