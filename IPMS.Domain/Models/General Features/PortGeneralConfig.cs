using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.Models
{
     [DataContract]
    public partial class PortGeneralConfig : EntityBase
    {
         [DataMember]
        public int PortGeneralConfigID { get; set; }
         [DataMember]
        public string PortCode { get; set; }
         [DataMember]
        public string ConfigName { get; set; }
         [DataMember]
        public string ConfigValue { get; set; }
         [DataMember]
        public string RecordStatus { get; set; }
         [DataMember]
        public int CreatedBy { get; set; }
         [DataMember]
        public System.DateTime CreatedDate { get; set; }
         [DataMember]
        public int ModifiedBy { get; set; }
         [DataMember]
        public System.DateTime ModifiedDate { get; set; }
         [DataMember]
        public string ConfigLabelName { get; set; }
         [DataMember]
        public string GroupName { get; set; }
         [DataMember]
        public virtual Port Port { get; set; }
         [DataMember]
        public virtual User User { get; set; }
         [DataMember]
        public virtual User User1 { get; set; }
    }
}
