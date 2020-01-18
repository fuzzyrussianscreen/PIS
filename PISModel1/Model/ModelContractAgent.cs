using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace PISModel1.Model
{
    [DataContract]
    public class ModelContractAgent
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int AgentId { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public bool Status { get; set; }
        [DataMember]
        public string Commission { get; set; }
        public virtual ModelUser Agent { get; set; }

        [ForeignKey("AgentId")]
        public virtual List<ModelContractClient> ContractsClient { get; set; }
    }
}
