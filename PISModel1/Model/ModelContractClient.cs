using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace PISModel1.Model
{
    [DataContract]
    public class ModelContractClient
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int AgentId { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public double Sum { get; set; }
        [DataMember]
        public DateTime DateStart { get; set; }
        [DataMember]
        public DateTime DateEnd { get; set; }
        public virtual ModelContractAgent 
            Agent { get; set; }
        public virtual ModelClient 
            Client { get; set; }
        [ForeignKey("ContractId")]
        public virtual List<ModelDirectory> 
            Directories { get; set; }
    }
}
