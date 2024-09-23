using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class SuppServiceRequest : EntityBase
    {
        public SuppServiceRequest()
        {
            this.SuppFloatingCranes = new List<SuppFloatingCrane>();
            this.SuppHotColdWorkPermits = new List<SuppHotColdWorkPermit>();          
            this.SuppHotWorkInspections = new List<SuppHotWorkInspection>();          
        }

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
        public System.DateTime FromDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ToDate { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public Nullable<long> Quantity { get; set; }
        [DataMember]
        public string TermsandConditions { get; set; }
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
        public  ArrivalNotification ArrivalNotification { get; set; }
        [DataMember]
        public  Berth Berth { get; set; }
        [DataMember]
        public  SubCategory SubCategory { get; set; }
        [DataMember]
        public  ICollection<SuppFloatingCrane> SuppFloatingCranes { get; set; }
        [DataMember]
        public  ICollection<SuppHotColdWorkPermit> SuppHotColdWorkPermits { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  WorkflowInstance WorkflowInstance { get; set; }

        [DataMember]
        public  ICollection<SuppHotWorkInspection> SuppHotWorkInspections { get; set; }       

        //-- Added by sandeep on 04-02-2015
        [NotMapped]
        public string ServiceTypeName { get; set; }
        //-- end

        //---Added by Omprakash on 03-May-2015
        [DataMember]
        public Nullable<int> AgentId { get; set; }

        //---Added by Omprakash on 03-May-2015
        [DataMember]
        public  Agent Agent { get; set; }
        [NotMapped]
        public string IsPrimaryAgent { get; set; }

        //-- Added by sandeep on 13-07-2015
        [DataMember]
        public string IsStartTime { get; set; }
        //-- end
        [NotMapped]
        public System.DateTime SubmittedDateTime { get; set; }

        [NotMapped]
        public string VesselName { get; set; }        
    }
}
