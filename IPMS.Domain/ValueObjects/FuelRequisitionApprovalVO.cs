using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public partial class FuelRequisitionApprovalVO
    {


        [DataMember]
        public int FuelRequisitionApprovalID { get; set; }
        [DataMember]
        public int FuelRequisitionID { get; set; }
        [DataMember]
        public string WFStatus { get; set; }
        [DataMember]
        public int ApprovedBy { get; set; }
        [DataMember]
        public string ApprovedDate { get; set; }
        [DataMember]
        public string RejectComments { get; set; }
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

    }
}
