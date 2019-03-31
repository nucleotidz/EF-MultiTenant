using Multi.Tenant.Domain.Config;
using Multi.Tenant.Domain.Factory;
using Multi.Tenant.Domain.OLTP;
using System;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;

namespace Multi.Tenanat.App.Factory
{

    public class DataContextFactory: IDataContextFactory,IDisposable
    {
        private bool _disposed;
        public OLTPContext GetDbContext(int engagementid)
        {
            string constring = this.GetConnectionString(engagementid);
            return new OLTPContext(constring);
        }

        private string GetConnectionString(int engagementid)
        {
            string efCon = "metadata=res://*/OLTP.OLTPModel.csdl|res://*/OLTP.OLTPModel.ssdl|res://*/OLTP.OLTPModel.msl;provider=System.Data.SqlClient;";
            using (CONFIGDBContext cob = new CONFIGDBContext())
            {
                Engagement engagement = cob.Engagements.Where(x => x.Id == engagementid).Select(x => x).FirstOrDefault();
                var dataSource = engagement.EngagementDBServerName;
                var initialCatalog = engagement.DBName;
                var conUsername = engagement.EngagementDBLogin;
                var conUserPWD = "S!wan@2461511809";
                var EngDBConnTimeout = 300;
                SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder
                {
                    DataSource = dataSource,
                    InitialCatalog = initialCatalog,
                    MultipleActiveResultSets = true,
                    IntegratedSecurity = false,
                    UserID = conUsername,
                    Password = conUserPWD,
                    ConnectTimeout = EngDBConnTimeout
                };
                EntityConnectionStringBuilder builder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    ProviderConnectionString = sqlBuilder.ConnectionString
                };

                return efCon + builder.ConnectionString;
            }
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
                   // this._unitOfWork.Dispose();
                }
            }
            this._disposed = true;
        }
    }
}
