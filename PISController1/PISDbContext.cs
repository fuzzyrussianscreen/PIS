using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;
using PISModel1;
using PISModel1.Model;

namespace PISController1
{
    public class PISDbContext : DbContext
    {
        public PISDbContext() : base("PISDatabase2")
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied =
           System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public virtual DbSet<ModelUser> Users { get; set; }
        public virtual DbSet<ModelClient> Clients { get; set; } 
        public virtual DbSet<ModelContractAgent> ContractAgents { get; set; }
        public virtual DbSet<ModelContractClient> ContractClients { get; set; }
        public virtual DbSet<ModelDirectory> Directories { get; set; }
        public virtual DbSet<ModelServices> Services { get; set; }
        public virtual DbSet<ModelStatements> Statements { get; set; }
        public virtual DbSet<ModelStatementsTab> StatementsTab { get; set; }
    }
}
