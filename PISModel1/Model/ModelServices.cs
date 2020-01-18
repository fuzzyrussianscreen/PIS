using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace PISModel1.Model
{
    [DataContract]
    public class ModelServices
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string NameService { get; set; }
        [DataMember]
        public string Price { get; set; }
        [DataMember]
        public string Description { get; set; }

        [ForeignKey("ServiceId")]
        public virtual List<ModelDirectory> Directories { get; set; }
    }
}
