using PISController1;
using PISModel1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PISController1.ControllerOther
{
    public class ContractAgent
    {
        private PISDbContext context;
        public ContractAgent(PISDbContext context)
        {
            this.context = context;
        }
        public List<ModelContractAgent> GetList()
        {
            List<ModelContractAgent> result = context.ContractAgents.AsEnumerable().Select(rec => new ModelContractAgent
            {
                Id = rec.Id,
                AgentId = rec.AgentId
            })
            .ToList();
            return result;
        }
    }
}