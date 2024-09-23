using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using IPMS.Domain.Models;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class SuppServiceRequestVO
    {
        [DataMember]
        public int SuppServiceRequestID { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public string ServiceType { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string QuayCode { get; set; }
        [DataMember]
        public string BerthCode { get; set; }
        [DataMember]
        public string FromDate { get; set; }
        [DataMember]
        public string ToDate { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public Nullable<long> Quantity { get; set; }
        [DataMember]
        public bool TermsandConditions { get; set; }
        [DataMember]
        public Nullable<int> WorkflowInstanceID { get; set; }
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
        public virtual ArrivalNotificationVO ArrivalNotification { get; set; }
        [DataMember]
        public virtual List<SuppFloatingCraneVO> SuppFloatingCranesVO { get; set; }
        [DataMember]
        public virtual SuppHotColdWorkPermitVO SuppHotColdWorkPermitsVO { get; set; }
        //[DataMember]
        //public List<ArrivalCommodityVo> ArrivalCommoditiesVO { get; set; }
        [DataMember]
        public string VesselName { get; set; }
        [DataMember]
        public string ServiceTypeName { get; set; }
        [DataMember]
        public string BerthKey { get; set; }
        [DataMember]
        public SuppHotWorkInspectionVO SuppHotWorkInspectionVO { get; set; }
        [DataMember]
        public string BerthName { get; set; }
        [DataMember]
        public string WFStatus { get; set; }

        // -- Added by sandeep on 15th Sep 2014

        [DataMember]
        public string RequestNo { get; set; }
        [DataMember]
        public string VesselType { get; set; }
        [DataMember]
        public string VesselDetails { get; set; }
        [DataMember]
        public SuppDockUnDockTimeVO SuppDockUnDockTimenVO { get; set; }
        // -- end
        [DataMember]
        public Nullable<System.DateTime> ETB { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ETUB { get; set; }
        //-- Added by sandeep on 21-02-2015
        [DataMember]
        public string AnyDangerousGoods { get; set; }
        //-- end

        [DataMember]
        public string WorkflowRemarks { get; set; }

        //---Added by Omprakash on 03-May-2015
        [DataMember]
        public Nullable<int> AgentId { get; set; }
        [DataMember]
        public string IsPrimaryAgent { get; set; }

        //-- Added by sandeep on 13-07-2015
        [DataMember]
        public bool IsStartTime { get; set; }

        [DataMember]
        public System.DateTime SubmittedDateTime { get; set; }
        [DataMember]
        public string ActionName { get; set; }

       
        //-- end
    }
}
