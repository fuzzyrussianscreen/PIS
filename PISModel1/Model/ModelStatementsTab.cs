using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace PISModel1.Model
{
    [DataContract]
    public class ModelStatementsTab
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int StatementId { get; set; }
        [DataMember]
        public int AgentId { get; set; }
        [DataMember]
        public double Sum { get; set; }

        public virtual ModelContractAgent Agent { get; set; }
        public virtual ModelStatements Statement { get; set; }
    }
}
