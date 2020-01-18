using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace PISModel1.Model
{
    [DataContract]
    public class ModelClient
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FIO { get; set; }
        [DataMember]
        public string Passport { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [ForeignKey("ClientId")]
        public virtual List<ModelContractClient> 
            ContractsClient { get; set; }
    }
}
