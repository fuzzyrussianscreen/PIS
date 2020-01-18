using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PISModel1.Model
{
    [DataContract]
    public class ModelDirectory
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ServiceId { get; set; }
        [DataMember]
        public int ContractId { get; set; }
        [DataMember]
        public DateTime Data { get; set; }
        public virtual ModelServices Services { get; set; }
        public virtual ModelContractClient Contract { get; set; }
    }
}
