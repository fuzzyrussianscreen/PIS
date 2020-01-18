using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace PISModel1.Model
{
    [DataContract]
    public class ModelStatements
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public string Period { get; set; }

        [ForeignKey("StatementId")]
        public virtual List<ModelStatementsTab> StatementsTab { get; set; }
    }
}
