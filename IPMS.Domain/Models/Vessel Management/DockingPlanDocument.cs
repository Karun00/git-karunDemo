using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class DockingPlanDocument : EntityBase
    {
        [DataMember]
        public int DockingPlanDocumentID { get; set; }
        [DataMember]
        public int DockingPlanID { get; set; }
        [DataMember]
        public int DocumentID { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }

        //public int CreatedBy { get; set; }
        //public System.DateTime CreatedDate { get; set; }
        //public int ModifiedBy { get; set; }
        //public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public virtual DockingPlan DockingPlan { get; set; }
        //public virtual User User { get; set; }
        //public virtual User User1 { get; set; }
        [DataMember]
        public virtual Document Document { get; set; }
    }
}
