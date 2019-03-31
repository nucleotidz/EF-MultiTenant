using Multi.Tenant.Domain.OLTP;
using System;

namespace Multi.Tenant.Domain.Factory
{
    public interface IDataContextFactory: IDisposable
    {
        OLTPContext GetDbContext(int engagementid);
    }
}
